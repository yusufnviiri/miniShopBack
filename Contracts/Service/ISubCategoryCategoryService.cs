using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ISubCategoryCategoryService
    {

        Task<IEnumerable<CategoryDto>> GetAllSubCategoryCategoriesAsync();
        Task<SubCategoryCategory?> FindSubCategoryCategoryByIdAsync(int categoryId, bool tracking);
        Task<IEnumerable<CategoryDto>> FindSubCategoryCategoryByNameAsync(string categoryName, bool tracking);
        Task CreateSubCategoryCategoryAsync(SubCategoryCategory category);
        Task UpdateSubCategoryCategoryAsync(SubCategoryCategory category);
        Task DeleteSubCategoryCategoryAsync(int subCategoryCategoryId);
        Task<IEnumerable<SubCategoryCategoryRefDto>> FindSubCategoryCategoryRefByIdAsync(int subCategoryId);

    }
}
