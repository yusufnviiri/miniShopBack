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

namespace Repository.Repos
{
    public class ProductAttributeValueRepo:RepositoryBase<ProductAttributeValue>, IProductAttributeValueRepo
    {
        public ProductAttributeValueRepo(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
      

        public async Task<IEnumerable<ProductAttributeValueDto>> GetAllProductAttributeValues()
        {
            return await FindAll(false).Select(pv => new ProductAttributeValueDto()
            {
                ProductId = pv.ProductId,
                ProductAttributeValueId = pv.ProductAttributeValueId,
                CategoryAttributeId = pv.CategoryAttributeId,
                StringValue = pv.StringValue,
                IntValue = pv.IntValue,
                DecimalValue = pv.DecimalValue,
                BoolValue = pv.BoolValue,
                DateOnlyValue=pv.DateValue,

            }).ToListAsync();
        }
       public async Task<ProductAttributeValue?> FindAProductAttributeValue(int productAttributeValueId, bool tracking)
        {
            return await FindByCondition(pv=>pv.ProductAttributeValueId == productAttributeValueId, tracking).FirstOrDefaultAsync();
        }
        public void CreateProductAttributeValue(ProductAttributeValue attributeValue)=>CreateBase(attributeValue);
        public void UpdateProductAttributeValue(ProductAttributeValue attributeValue)=> UpdateBase(attributeValue);
        public void DeleteProductAttributeValue(ProductAttributeValue attributeValue)=> DeleteBase(attributeValue);
    }
}
