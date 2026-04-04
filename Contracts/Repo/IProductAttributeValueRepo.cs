using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IProductAttributeValueRepo
    {
        Task<IEnumerable<ProductAttributeValueDto>> GetAllProductAttributeValues();
        Task<ProductAttributeValue?> FindAProductAttributeValue(int productAttributeValueId, bool tracking);
        void CreateProductAttributeValue(ProductAttributeValue attributeValue );
        void UpdateProductAttributeValue(ProductAttributeValue attributeValue);
        void DeleteProductAttributeValue(ProductAttributeValue attributeValue);
    }
}
