using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class ProductImpressionRepo : RepositoryBase<ProductImpression>, IProductImpressionRepo
    {
        public ProductImpressionRepo(ApplicationDbContext _db) : base(_db)
        {

        }

       public async Task<IEnumerable<ProductImpression>> GetAllProductImpressions() => await FindAll(false).ToListAsync();
        public  IQueryable<ProductImpression> ProductImpressionsQueryData()=>FindAll(false);
        public async Task<ProductImpression?> FindProductImpressionById(Guid productImpressionId, bool tracking) => await FindByCondition(p => p.ProductImpressionId == productImpressionId, tracking).FirstOrDefaultAsync();
        public async Task<ProductImpression?> FindProductImpressionByProductId(Guid productId, bool tracking) => await FindByCondition(p => p.ProductId == productId, tracking).FirstOrDefaultAsync();
        public async Task<ProductImpression?> FindProductImpressionForUpdate(Guid productImpressionId)=>await FindByCondition(p=>p.ProductImpressionId==productImpressionId,true).FirstOrDefaultAsync();
        public void CreateProductImpression(ProductImpression impression)=>CreateBase(impression);
        public void UpdateProductImpression(ProductImpression impression)=>UpdateBase(impression);
        public void DeleteProductImpression(ProductImpression impression)=>DeleteBase(impression);
    }
}
