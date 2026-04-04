using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ICategoryAttributeService
    {
        Task<IEnumerable<CategoryAttributeDto>> GetAllCategoryAttributesAsync();
        Task<IReadOnlyList<CategoryAttributeDto>> FindCategoryCategoryAttributesAsync(int CategoryId);
        Task<CategoryAttribute?> FindCategoryAttributeAsync(int CategoryAttributeId, bool tracking);
        Task CreateCategoryAttributeAsync(CategoryAttribute categoryAttribute);
        Task UpdateCategoryAttributeAsync(CategoryAttribute attribute);
        Task DeleteCategoryAttributeAsync(int categoryId);
    }
}
