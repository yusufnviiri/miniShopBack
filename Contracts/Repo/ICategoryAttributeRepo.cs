using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ICategoryAttributeRepo
    {
        Task<IEnumerable<CategoryAttributeDto>> GetAllCategoryAttributes();
        Task<CategoryAttribute?> FindCategoryAttribute(int CategoryAttributeId, bool tracking);
        Task<CategoryAttributeDto?> FindCategoryAttributerRef(int CategoryAttributeId, bool tracking);

        Task<IReadOnlyList<CategoryAttributeDto>> FindCategoryCategoryAttributes(
       int categoryId, CancellationToken cancellationToken = default);
        void CreateCategoryAttribute(CategoryAttribute categoryAttribute);
        void UpdateCategoryAttribute(CategoryAttribute attribute );
        void DeleteCategoryAttribute(CategoryAttribute category);
    }
}
