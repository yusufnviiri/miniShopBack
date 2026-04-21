using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IProductImpressionRepo
    {
        Task<IEnumerable<ProductImpression>> GetAllProductImpressions();
        IQueryable<ProductImpression> ProductImpressionsQueryData();

        Task<ProductImpression?> FindProductImpressionById(Guid productImpressionId, bool tracking);
        Task<ProductImpression?> FindProductImpressionByProductId(Guid productId, bool tracking);
        Task<ProductImpression?> FindProductImpressionForUpdate(Guid productImpressionId);
        void CreateProductImpression(ProductImpression impression );
        void UpdateProductImpression(ProductImpression impression);
        void DeleteProductImpression(ProductImpression impression);
    }
}
