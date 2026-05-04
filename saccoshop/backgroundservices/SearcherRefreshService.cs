using Microsoft.Extensions.Options;
using Repository.lucene;

namespace saccoshop.backgroundservices
{
    /// <summary>
    /// Periodically refreshes every registered Lucene searcher so newly written
    /// documents become visible to queries. The refresh interval is the
    /// freshness vs CPU trade-off — 2-5 seconds is the typical sweet spot.
    /// </summary>
    public sealed class SearcherRefreshService : BackgroundService
    {
        private readonly ILuceneIndexRegistry _registry;
        private readonly LuceneOptions _options;
        private readonly ILogger<SearcherRefreshService> _logger;

        public SearcherRefreshService(
            ILuceneIndexRegistry registry,
            IOptions<LuceneOptions> options,
            ILogger<SearcherRefreshService> logger)
        {
            _registry = registry;
            _options = options.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var interval = TimeSpan.FromSeconds(Math.Max(1, _options.SearcherRefreshSeconds));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    foreach (var ctx in _registry.All)
                    {
                        ctx.MaybeRefresh();
                    }
                }
                catch (Exception ex)
                {
                    // Refresh failure is non-fatal — searches continue to work
                    // against the old snapshot until next refresh succeeds.
                    _logger.LogError(ex, "Searcher refresh failed");
                }

                try
                {
                    await Task.Delay(interval, stoppingToken);
                }
                catch (TaskCanceledException) { /* shutting down */ }
            }
        }
    }
}
