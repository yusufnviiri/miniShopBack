using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface ITradeIndexer
    {
        ValueTask QueueIndexAsync(Guid tradeId, CancellationToken ct = default);
        ValueTask QueueRemoveAsync(Guid tradeId, CancellationToken ct = default);
        ValueTask QueueReindexBySellerAsync(Guid sellerProfileId, CancellationToken ct = default);
    }
}
