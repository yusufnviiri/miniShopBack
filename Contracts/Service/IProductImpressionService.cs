using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IProductImpressionService
    {

        Task<IEnumerable<ProductImpression>> GetAllProductImpressionsAsync();

        Task<ProductImpression?> FindProductImpressionByIdAsync(Guid productImpressionId, bool tracking);
        Task<ProductImpression?> FindProductImpressionByProductIdAsync(Guid productId, bool tracking);
        Task CreateProductImpressionAsync(NewImpressionDto newImpression);
        Task UpdateProductImpressionAsync(ProductImpression impression);
        Task DeleteProductImpressionAsync(Guid productImpressionId);
    }
}
