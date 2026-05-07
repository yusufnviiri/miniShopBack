using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface ITradeSearchService
    {
        Task<TradeSearchResult> SearchAsync(
            TradeSearchRequest request,
            CancellationToken ct = default);
    }
}
