using Contracts.Lucene;
using Microsoft.Extensions.Options;
using Repository.lucene;
using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class TradeSearchService : ITradeSearchService
    {
        private readonly ITradeSearchRepository _repo;
        private readonly LuceneOptions _options;

        public TradeSearchService(
            ITradeSearchRepository repo,
            IOptions<LuceneOptions> options)
        {
            _repo = repo;
            _options = options.Value;
        }

        public Task<TradeSearchResult> SearchAsync(
            TradeSearchRequest request,
            CancellationToken ct = default)
        {
            var result = _repo.Search(request, _options.MaxPageSize);
            return Task.FromResult(result);
        }
    }
}
