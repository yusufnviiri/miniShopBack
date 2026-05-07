using Contracts.Lucene;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Services.Lucene;
using System;

namespace saccoshop.backgroundservices
{
    public sealed class TradeIndexingBackgroundService : BackgroundService
    {
        private readonly TradeIndexingQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TradeIndexingBackgroundService> _logger;

        private const int CommitEveryNJobs = 50;
        private static readonly TimeSpan CommitInterval = TimeSpan.FromSeconds(5);

        public TradeIndexingBackgroundService(
            TradeIndexingQueue queue,
            IServiceScopeFactory scopeFactory,
            ILogger<TradeIndexingBackgroundService> logger)
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
                        "Trade indexing job {JobType} for {Id} failed",
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

        private async Task ProcessJobAsync(TradeIndexingJob job, CancellationToken ct)
        {
            using var scope = _scopeFactory.CreateScope();
            var sp = scope.ServiceProvider;

            var repo = sp.GetRequiredService<ITradeSearchRepository>();
            var db = sp.GetRequiredService<ApplicationDbContext>();
            var mapper = sp.GetRequiredService<TradeDocumentMapper>();

            switch (job.Type)
            {
                case TradeIndexingJobType.IndexTrade:
                    await IndexOneAsync(job.Id, repo, db, mapper, ct);
                    break;

                case TradeIndexingJobType.RemoveTrade:
                    repo.Delete(job.Id);
                    break;

                case TradeIndexingJobType.ReindexBySeller:
                    await ReindexSellerAsync(job.Id, repo, db, mapper, ct);
                    break;
            }
        }

        private static async Task IndexOneAsync(
            Guid tradeId,
            ITradeSearchRepository repo,
            ApplicationDbContext db,
            TradeDocumentMapper mapper,
            CancellationToken ct)
        {
            var trade = await db.Trades.AsNoTracking()
                .Include(t => t.SellerProfile)
                .Include(t => t.Category)
                .Include(t => t.SubCategory)
                .Include(t => t.SubCategoryCategory)
                .Include(t => t.Images)
                .FirstOrDefaultAsync(t => t.TradeId == tradeId, ct);

            if (trade is null || trade.IsDeleted || !trade.IsActive)
            {
                repo.Delete(tradeId);
                return;
            }

            var agg = await db.TradeReviews
                .AsNoTracking()
                .Where(r => r.TradeId == tradeId)
                .GroupBy(r => 1)
                .Select(g => new
                {
                    Count = g.Count(),
                    Avg = (float)g.Average(r => (double)r.Rating)
                })
                .FirstOrDefaultAsync(ct);

            var dto = mapper.Map(
                trade,
                reviewCount: agg?.Count ?? 0,
                averageRating: agg?.Avg ?? 0f);

            repo.AddOrUpdate(dto);
        }

        private static async Task ReindexSellerAsync(
            Guid sellerProfileId,
            ITradeSearchRepository repo,
            ApplicationDbContext db,
            TradeDocumentMapper mapper,
            CancellationToken ct)
        {
            var ids = await db.Trades
                .AsNoTracking()
                .Where(t => t.SellerProfileId == sellerProfileId
                         && !t.IsDeleted && t.IsActive)
                .Select(t => t.TradeId)
                .ToListAsync(ct);

            foreach (var id in ids)
            {
                await IndexOneAsync(id, repo, db, mapper, ct);
            }
        }

        private void Commit()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<ITradeSearchRepository>();
                repo.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trade Lucene commit failed");
            }
        }
    }
}
