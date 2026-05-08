using Contracts.Lucene;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Services.Lucene;
using System;

namespace saccoshop.backgroundservices
{
    public sealed class UserGroupIndexingBackgroundService : BackgroundService
    {
        private readonly UserGroupIndexingQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<UserGroupIndexingBackgroundService> _logger;

        private const int CommitEveryNJobs = 50;
        private static readonly TimeSpan CommitInterval = TimeSpan.FromSeconds(5);

        public UserGroupIndexingBackgroundService(
            UserGroupIndexingQueue queue,
            IServiceScopeFactory scopeFactory,
            ILogger<UserGroupIndexingBackgroundService> logger)
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
                        "UserGroup indexing job {JobType} for {Id} failed",
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

        private async Task ProcessJobAsync(UserGroupIndexingJob job, CancellationToken ct)
        {
            using var scope = _scopeFactory.CreateScope();
            var sp = scope.ServiceProvider;

            var repo = sp.GetRequiredService<IUserGroupSearchRepository>();
            var db = sp.GetRequiredService<ApplicationDbContext>();
            var mapper = sp.GetRequiredService<UserGroupDocumentMapper>();

            switch (job.Type)
            {
                case UserGroupIndexingJobType.IndexGroup:
                    await IndexOneAsync(job.Id, repo, db, mapper, ct);
                    break;
                case UserGroupIndexingJobType.RemoveGroup:
                    repo.Delete(job.Id);
                    break;
            }
        }

        private static async Task IndexOneAsync(
            Guid groupId,
            IUserGroupSearchRepository repo,
            ApplicationDbContext db,
            UserGroupDocumentMapper mapper,
            CancellationToken ct)
        {
            var group = await db.UserGroups.AsNoTracking()
                .Include(g => g.GroupType)
                .Include(g => g.Address)
                .FirstOrDefaultAsync(g => g.UserGroupId == groupId, ct);

            if (group is null)
            {
                repo.Delete(groupId);
                return;
            }

            var memberCount = await db.GroupMembers
                .AsNoTracking()
                .CountAsync(gm => gm.UserGroupId == groupId, ct);

            var dto = mapper.Map(group, memberCount);
            repo.AddOrUpdate(dto);
        }

        private void Commit()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IUserGroupSearchRepository>();
                repo.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserGroup Lucene commit failed");
            }
        }
    }
}
