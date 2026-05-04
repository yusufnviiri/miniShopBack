using Contracts.Lucene;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public class IndexRebuildService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<IndexRebuildService> _logger;
        private readonly TimeSpan _rebuildInterval = TimeSpan.FromHours(6);

        public IndexRebuildService(IServiceProvider services,
                                   ILogger<IndexRebuildService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Rebuild immediately on startup
            await RebuildAsync();

            // Then rebuild on schedule
            using var timer = new PeriodicTimer(_rebuildInterval);
            while (await timer.WaitForNextTickAsync(stoppingToken))
                await RebuildAsync();
        }

        private async Task RebuildAsync()
        {
            try
            {
                _logger.LogInformation("Lucene index rebuild started at {Time}", DateTime.UtcNow);

                using var scope = _services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var indexService = scope.ServiceProvider.GetRequiredService<IProductIndexSearch>();

                var products = await db.Products.AsNoTracking()
                    
                    .Include(p => p.Category)
                    .Include(p => p.SubCategory)
                    .Include(p => p.SubCategoryCategory)
                    .Where(p => !p.IsDeleted)
                    .ToListAsync();

                indexService.RebuildIndex(products);

                _logger.LogInformation("Lucene index rebuilt — {Count} products indexed", products.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lucene index rebuild failed");
            }
        }
    }
}

