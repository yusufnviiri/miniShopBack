using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IProductAttributeValueService
    {
        Task<IEnumerable<ProductAttributeValueDto>> GetAllProductAttributeValuesAsync();
        Task<ProductAttributeValue?> FindAProductAttributeValueAsync(int productAttributeValueId, bool tracking);
        Task<ProductAttributeValue?> MapAttributeValueToCategoryAttribute(int categoryAttributeId, int? intValue = null, DateOnly? dateOnlyValue = null, string? stringValue = null, bool? boolValue = false);


        Task CreateProductAttributeValueAsync(ProductAttributeValue attributeValue);
        Task UpdateProductAttributeValueAsync(ProductAttributeValue attributeValue);
        Task DeleteProductAttributeValueAsync(int attributeValueId);
    }
}
