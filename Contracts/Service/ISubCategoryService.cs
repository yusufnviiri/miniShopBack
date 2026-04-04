using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<ShowAllCategoriesDto>> GetAllSubCategoriesAsync();
        Task<IEnumerable<CategoryDto>> GetAllSubCategoriesWithSubCategoriesAsync(bool tracking);
        Task<SubCategory?> FindSubCategoryByIdAsync(int categoryId, bool tracking);
        Task<SubCategory?> FindSubCategoryByIdWithSubCategoriesAsync(int subCategoryId);

        Task<IEnumerable<CategoryDto>> FindSubCategoryByNameAsync(string categoryName, bool tracking);
        Task CreateSubCategoryAsync(SubCategoryDto category);
        Task UpdateSubCategoryAsync(SubCategory category);
        Task DeleteSubCategory(int categoryId);
        Task<IEnumerable<SubCategoryRefDto>> FindCategorySubCategoryRefByIdAsync(int categoryId);
        Task<IEnumerable<MiniSubCategoryDto>> GetAllSubCategoriesInDbAsync();

    }
}
