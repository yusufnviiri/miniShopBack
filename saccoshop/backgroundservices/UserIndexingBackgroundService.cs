using Contracts.Lucene;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Services.Lucene;
using System;

namespace saccoshop.backgroundservices
{
    public sealed class UserIndexingBackgroundService : BackgroundService
    {
        private readonly UserIndexingQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<UserIndexingBackgroundService> _logger;

        private const int CommitEveryNJobs = 50;
        private static readonly TimeSpan CommitInterval = TimeSpan.FromSeconds(5);

        public UserIndexingBackgroundService(
            UserIndexingQueue queue,
            IServiceScopeFactory scopeFactory,
            ILogger<UserIndexingBackgroundService> logger)
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var pending = 0;
            var lastCommit = DateTime.UtcNow;

            await foreach (var job in _queue.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await ProcessJobAsync(job, stoppingToken);
                    pending++;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "User indexing job {JobType} for {Id} failed",
                        job.Type, job.Id);
                }

                if (pending >= CommitEveryNJobs ||
                    DateTime.UtcNow - lastCommit > CommitInterval)
                {
                    Commit();
                    pending = 0;
                    lastCommit = DateTime.UtcNow;
                }
            }

            Commit();
        }

        private async Task ProcessJobAsync(UserIndexingJob job, CancellationToken ct)
        {
            using var scope = _scopeFactory.CreateScope();
            var sp = scope.ServiceProvider;

            var repo = sp.GetRequiredService<IUserSearchRepository>();
            var db = sp.GetRequiredService<ApplicationDbContext>();
            var mapper = sp.GetRequiredService<UserDocumentMapper>();

            switch (job.Type)
            {
                case UserIndexingJobType.IndexUser:
                    await IndexOneAsync(job.Id, repo, db, mapper, ct);
                    break;

                case UserIndexingJobType.RemoveUser:
                    repo.Delete(job.Id);
                    break;

                case UserIndexingJobType.ReindexByUserGroup:
                    await ReindexByGroupAsync(job.Id, repo, db, mapper, ct);
                    break;
            }
        }

        private static async Task IndexOneAsync(
            Guid userProfileId,
            IUserSearchRepository repo,
            ApplicationDbContext db,
            UserDocumentMapper mapper,
            CancellationToken ct)
        {
            var profile = await db.UserProfiles.AsNoTracking()
                .Include(p => p.IdentityUser)
                .Include(p => p.Address)
                .Include(p => p.SellerProfile)
                .FirstOrDefaultAsync(p => p.UserProfileId == userProfileId, ct);

            if (profile is null)
            {
                repo.Delete(userProfileId);
                return;
            }

            // Group names — separate query so we can shape it
            var groupNames = await db.GroupMembers
                .AsNoTracking()
                .Where(gm => gm.UserProfileId == userProfileId)
                .Select(gm => gm.Group.UserGroupName)  // adjust nav name to your model
                .ToListAsync(ct);

            // Activity counts — three small COUNT queries.
            // If perf becomes a concern with many users, denormalize these onto
            // UserProfile and update incrementally.
            var productCount = profile.SellerProfileId.HasValue
                ? await db.Products
                    .AsNoTracking()
                    .CountAsync(p => p.SellerProfileId == profile.SellerProfileId.Value
                                  && p.IsActive && !p.IsDeleted, ct)
                : 0;

            var tradeCount = profile.SellerProfileId.HasValue
                ? await db.Trades
                    .AsNoTracking()
                    .CountAsync(t => t.SellerProfileId == profile.SellerProfileId.Value
                                  && t.IsActive && !t.IsDeleted, ct)
                : 0;

            var dto = mapper.Map(
                profile,
                groupNames,
                userGroupCount: groupNames.Count,
                productCount: productCount,
                tradeCount: tradeCount);

            repo.AddOrUpdate(dto);
        }

        private static async Task ReindexByGroupAsync(
            Guid userGroupId,
            IUserSearchRepository repo,
            ApplicationDbContext db,
            UserDocumentMapper mapper,
            CancellationToken ct)
        {
            var memberIds = await db.GroupMembers
                .AsNoTracking()
                .Where(gm => gm.UserGroupId == userGroupId)
                .Select(gm => gm.UserProfileId)
                .ToListAsync(ct);

            foreach (var id in memberIds)
            {
                await IndexOneAsync(id, repo, db, mapper, ct);
            }
        }

        private void Commit()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IUserSearchRepository>();
                repo.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User Lucene commit failed");
            }
        }
    }
}
