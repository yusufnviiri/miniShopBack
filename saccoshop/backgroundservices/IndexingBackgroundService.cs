using Contracts.Lucene;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Services.Lucene;
using System;

namespace saccoshop.backgroundservices
{
    public sealed class IndexingBackgroundService : BackgroundService
    {
        private readonly IndexingQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<IndexingBackgroundService> _logger;

        // Commit every N jobs OR every M seconds, whichever first.
        private const int CommitEveryNJobs = 50;
        private static readonly TimeSpan CommitInterval = TimeSpan.FromSeconds(5);

        public IndexingBackgroundService(
            IndexingQueue queue,
            IServiceScopeFactory scopeFactory,
            ILogger<IndexingBackgroundService> logger)
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
                    // Indexing failures must never crash the app or block the queue.
                    // Log and continue. The nightly reconciliation job will heal drift.
                    _logger.LogError(ex,
                        "Indexing job {JobType} for {Id} failed",
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

            // Final flush on shutdown.
            Commit();
        }

        private async Task ProcessJobAsync(IndexingJob job, CancellationToken ct)
        {
            // Each job runs in its own DI scope so we get a fresh DbContext.
            using var scope = _scopeFactory.CreateScope();
            var sp = scope.ServiceProvider;

            var repo = sp.GetRequiredService<IProductSearchRepository>();
            var db = sp.GetRequiredService<ApplicationDbContext>(); // your DbContext type
            var mapper = sp.GetRequiredService<ProductDocumentMapper>();

            switch (job.Type)
            {
                case IndexingJobType.IndexProduct:
                    await IndexOneAsync(job.Id, repo, db, mapper, ct);
                    break;

                case IndexingJobType.RemoveProduct:
                    repo.Delete(job.Id);
                    break;

                case IndexingJobType.ReindexBySeller:
                    await ReindexSellerAsync(job.Id, repo, db, mapper, ct);
                    break;
            }
        }

        private static async Task IndexOneAsync(
            Guid productId,
            IProductSearchRepository repo,
            ApplicationDbContext db,
            ProductDocumentMapper mapper,
            CancellationToken ct)
        {
            var product = await db.Products.AsNoTracking()
                .Include(p => p.SellerProfile)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Include(p => p.SubCategoryCategory)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.ProductId == productId, ct);

            // If the product no longer exists or is soft-deleted/inactive,
            // remove it from the index instead of indexing.
            if (product is null || product.IsDeleted || !product.IsActive)
            {
                repo.Delete(productId);
                return;
            }

            // Aggregate query — one round trip.
            var agg = await db.ProductReviews
                .AsNoTracking()
                .Where(r => r.ProductId == productId)
                .GroupBy(r => 1)
                .Select(g => new
                {
                    Count = g.Count(),
                    Avg = (float)g.Average(r => (double)r.Rating) // adjust to your Rating type
                })
                .FirstOrDefaultAsync(ct);

            var dto = mapper.Map(
                product,
                reviewCount: agg?.Count ?? 0,
                averageRating: agg?.Avg ?? 0f);

            repo.AddOrUpdate(dto);
        }

        private static async Task ReindexSellerAsync(
            Guid sellerProfileId,
            IProductSearchRepository repo,
            ApplicationDbContext db,
            ProductDocumentMapper mapper,
            CancellationToken ct)
        {
            // Stream IDs to avoid loading thousands of products into memory at once.
            var ids = await db.Products
                .AsNoTracking()
                .Where(p => p.SellerProfileId == sellerProfileId
                         && !p.IsDeleted && p.IsActive)
                .Select(p => p.ProductId)
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
                var repo = scope.ServiceProvider.GetRequiredService<IProductSearchRepository>();
                repo.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lucene commit failed");
            }
        }
    }
}
