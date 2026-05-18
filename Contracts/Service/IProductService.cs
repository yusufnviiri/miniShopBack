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
        Task<(ICollection<HomePageProductDto> productsData, MetaData MetaData)> GetHomePageProductsAsync(ProductRequestParameters requestParameters, CancellationToken ct = default);

        Task<(IEnumerable<HomePageProductDto> productsData, MetaData MetaData)>
    GetLatestFeaturedProductsAsync(ProductRequestParameters parameters);
        Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProductsByCategoryAsync( string categoryName);
        Task<ShowProductDto?> FindProductByIdAsync(bool tracking, Guid productId);
        Task<ProductDataDto?> GetProductDataAsync(Guid productId);
        Task<ProductDataDto?> GetProductDataBySlugNameAsync(string slugName);
        Task ToggleProductFeaturedState(Guid productId);
        Task MakeAllProductsFeatured();
        Task<Product> CreateProductAsync(NewProductDto product, CancellationToken ct = default);
        Task UpdateProductAsync(NewProductDto product, CancellationToken ct = default);
        Task DeleteProductAsync(Guid productId, CancellationToken ct = default);
        Task<ShowProductDto?> FindSellerProductAsync(Guid productId);
        Task<ShowProductDto?> FindSellerProductBySlugAsync(string slug);

        Task SetProductOldPriceAsync(Guid productId,decimal oldPrice);
        Task UpdateProductPriceAsync(Guid productId, decimal newPrice);
        Task UpdateProductDescription(SharedUpdatesDto sharedUpdates);
        Task<HomePageCustomProductsDto?> HomePageCustomProductsAsync();
        Task<List<SlugInfo>> GetAllProductSlugsAsync();
        Task<ProductPreviewDto?> FindProductBySlugForPreviewAsync(string slug);

    }
}
