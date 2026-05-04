using Contracts.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class ProductIndexer : IProductIndexer
    {
        private readonly IndexingQueue _queue;

        public ProductIndexer(IndexingQueue queue) => _queue = queue;

        public ValueTask QueueIndexAsync(Guid productId, CancellationToken ct = default)
            => _queue.EnqueueAsync(new IndexingJob(IndexingJobType.IndexProduct, productId), ct);

        public ValueTask QueueRemoveAsync(Guid productId, CancellationToken ct = default)
            => _queue.EnqueueAsync(new IndexingJob(IndexingJobType.RemoveProduct, productId), ct);

        public ValueTask QueueReindexBySellerAsync(Guid sellerProfileId, CancellationToken ct = default)
            => _queue.EnqueueAsync(new IndexingJob(IndexingJobType.ReindexBySeller, sellerProfileId), ct);
    }
}
