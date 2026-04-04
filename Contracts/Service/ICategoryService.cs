using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<ShowAllCategoriesDto>> GetAllCategoriesAsync();
        Task<IEnumerable<CategoryRefDto>> GetAllTradeCategoriesAsync();
        Task<IEnumerable<CategoryRefDto>> TradeCategoriesAsync();
        Task<IEnumerable<CategoryRefDto>> ProductCategoriesAsync();

        Task<CategoryDto?> FindCategoryWithSubCategoriesAsync(int categoryId);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesWithSubCategoriesAsync();
        Task CreateCategoryAsync(CategoryDto category);
        Task UpdateCategoryAsync(CategoryDto category);
        Task DeleteCategoryAsync(int categoryId);
        Task<Category?> FindCategoryDtoByIdAsync(int categoryId, bool tracking);
        Task<IEnumerable<CategoryRefDto>> GetAllCategoryReferencesAsync();
        Task<IEnumerable<MiniCategoryDto>> GetAllCategoriesInDbAsync();
        Task<AllCategoriesRef> GetAllCategoriesRefInDbAsync();
        Task<CategoriesSubCategoryCategoryDto> GetAllCategorySeedDataAsync();





    }
}
