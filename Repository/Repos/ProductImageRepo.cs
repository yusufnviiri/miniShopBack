using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Repository.Repos
{
    public class ProductImageRepo : RepositoryBase<ProductImage>, IProductImageRepo
    {
        public ProductImageRepo(ApplicationDbContext _db) : base(_db)
        {

        }

       public async Task<IEnumerable<ProductImageRefDto?>> GetAllProductImages()
        {
            {
                {
                    return await FindAll(false).Select(k => new ProductImageRefDto()
                    {
                        ProductId =(Guid)k.ProductId,
                        ProductImageId = k.ProductImageId,
                        IsPrimary = k.IsPrimary,


                    }).ToListAsync();
                }
            }
        }
        public async Task<ProductImage?> FindProductImageById(Guid imageId, bool tracking)
        {
            return await FindByCondition(p => p.ProductImageId == imageId, tracking).FirstOrDefaultAsync();
        }
        public async  Task<ProductImage?> GetPrimaryImage(Guid productId,bool tracking)
        
            {
                return await FindByCondition(p => p.IsPrimary == true && p.ProductId == productId, tracking).FirstOrDefaultAsync();
            }
        
        public async Task<ProductImage?> FindProductImageByProductId(Guid imageId, Guid productId, bool tracking)
        {
             return await FindByCondition(p=>p.ProductImageId==imageId&&p.ProductId==productId,tracking).FirstOrDefaultAsync();
        }
        public void CreateProductImage(ProductImage image)=> CreateBase(image);
        public void UpdateProductImage(ProductImage image)=>UpdateBase(image);
        public void DeleteProductImage(ProductImage image)=>DeleteBase(image);
     
       

    }
}