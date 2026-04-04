using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImageRefDto?>> GetAllProductImagesAsync();
        Task<ProductImage?> FindProductImageByIdAsync(Guid imageId, Guid productId, bool tracking);
        Task MakeImagePrimaryAsync(MiniProductImageDto miniProduct);

        Task<ProductImage?> GetPrimaryImageAsync(Guid productId, bool tracking);
        Task CreateProductImageAsync(ProductImageDto image);
        Task CreateProductImageListAsync(ICollection<ProductImage> images);

        Task UpdateProductImageAsync(ProductImageDto image);
        Task DeleteProductImageAsync(Guid imageId);

        Task DeleteProductImageIdRefAsync(Guid imageId, Guid ProductId);



    }
}
