using Contracts.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{

    public sealed class TradeIndexer : ITradeIndexer
    {
        private readonly TradeIndexingQueue _queue;

        public TradeIndexer(TradeIndexingQueue queue) => _queue = queue;

        public ValueTask QueueIndexAsync(Guid tradeId, CancellationToken ct = default)
        {
            if (tradeId == Guid.Empty)
                throw new ArgumentException("TradeId cannot be empty.", nameof(tradeId));
            return _queue.EnqueueAsync(new TradeIndexingJob(TradeIndexingJobType.IndexTrade, tradeId), ct);
        }

        public ValueTask QueueRemoveAsync(Guid tradeId, CancellationToken ct = default)
        {
            if (tradeId == Guid.Empty)
                throw new ArgumentException("TradeId cannot be empty.", nameof(tradeId));
            return _queue.EnqueueAsync(new TradeIndexingJob(TradeIndexingJobType.RemoveTrade, tradeId), ct);
        }

        public ValueTask QueueReindexBySellerAsync(Guid sellerProfileId, CancellationToken ct = default)
        {
            if (sellerProfileId == Guid.Empty)
                throw new ArgumentException("SellerProfileId cannot be empty.", nameof(sellerProfileId));
            return _queue.EnqueueAsync(new TradeIndexingJob(TradeIndexingJobType.ReindexBySeller, sellerProfileId), ct);
        }
    }
}
