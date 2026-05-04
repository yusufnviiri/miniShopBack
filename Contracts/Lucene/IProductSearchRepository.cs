using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    /// <summary>
    /// Lucene-backed repository for product search.
    /// Hides Lucene types from the rest of the app.
    /// </summary>
    public interface IProductSearchRepository
    {
        /// <summary>
        /// Add a new product or replace an existing one (atomic by ProductId).
        /// </summary>
        void AddOrUpdate(ProductIndexDocument document);
        void Delete(Guid productId);
        void DeleteBySeller(Guid sellerProfileId);
        void Commit();

        /// <summary>
        /// Run a query and return projected cards plus total hit count.
        /// </summary>      

        ProductSearchResult Search(ProductSearchRequest request, int maxPageSize);
    }

    /// <summary>Delete a product from the index by ID.</summary>

    /// <summary>Delete all products owned by a seller.</summary>

    /// <summary>Commit pending writes to disk. Call sparingly (it's expensive).</summary>

    // (Search method comes in the next step, when we build the read side.)
}

