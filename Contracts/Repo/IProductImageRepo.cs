using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IProductImageRepo
    {
        Task<IEnumerable<ProductImageRefDto?>> GetAllProductImages();
        Task<ProductImage?> FindProductImageByProductId(Guid imageId, Guid productId, bool tracking);
        Task<ProductImage?> FindProductImageById(Guid imageId, bool tracking);
        Task<ProductImage?> GetPrimaryImage(Guid productId, bool tracking);
        void CreateProductImage(ProductImage image );
        void UpdateProductImage(ProductImage image);
        void DeleteProductImage(ProductImage image);

    }
}
