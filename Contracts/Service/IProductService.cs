using Entities.Models;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProductsAsync();
        Task<(ICollection<HomePageProductDto> productsData, MetaData MetaData)> GetHomePageProductsAsync(ProductRequestParameters requestParameters);
        Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProductsByCategoryAsync( string categoryName);
        Task<ShowProductDto?> FindProductByIdAsync(bool tracking, Guid productId);
        Task<ProductDataDto?> GetProductDataAsync(Guid productId);
        void MakeProductFeautured(Guid productId);
        void MakeAllProductsFeautured();
        Task<Product> CreateProductAsync(NewProductDto product);
        Task UpdateProductAsync(NewProductDto product);
        Task DeleteProductAsync(Guid productId);
        Task<double> GetProductStockQuantityAsync(Guid productId);
        Task UpdateProductStockLevelAsync(Guid productId, double stockQuantity);
        Task<ShowProductDto?> FindSellerProductAsync(Guid productId);
        Task SetProductOldPriceAsync(Guid productId,decimal oldPrice);
        Task UpdateProductPriceAsync(Guid productId, decimal newPrice);
        Task UpdateProductStockAsync(Guid productId, int newStockQuantity);





    }
}
