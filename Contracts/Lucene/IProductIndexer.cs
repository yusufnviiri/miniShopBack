using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    /// <summary>
    /// Called by product/seller services when data changes.
    /// All methods enqueue work for the background indexer — they return fast
    /// and never block the calling request.
    /// </summary>
    public interface IProductIndexer
    {
        /// <summary>Queue a single product for (re)indexing.</summary>
        ValueTask QueueIndexAsync(Guid productId, CancellationToken ct = default);

        /// <summary>Queue a single product for removal from the index.</summary>
        ValueTask QueueRemoveAsync(Guid productId, CancellationToken ct = default);

        /// <summary>
        /// Queue all products owned by a seller for re-indexing.
        /// Used when seller fields (SellerName, IsVerified, Tier) change,
        /// since those are denormalized into product documents (Pattern B tax).
        /// </summary>
        ValueTask QueueReindexBySellerAsync(Guid sellerProfileId, CancellationToken ct = default);
    }
}
