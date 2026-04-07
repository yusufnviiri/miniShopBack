using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<ShowAllCategoriesDto>> GetAllCategories();
        IQueryable<Category> CategoriesQueryData();
        Task<IEnumerable<CategorySeedDto?>> GetCategorySeedData();
        Task<IEnumerable<CategoryRefDto>> TradeCategories();
        Task<IEnumerable<CategoryRefDto>> ProductCategories();
        Task<IEnumerable<CategoryRefDto>> GetAllTradeCategories();
        Task<IEnumerable<MiniCategoryDto>> GetAllCategoriesInDb();
        Task<IEnumerable<CategoryRefDto>> GetAllCategoryReferences();
        Task<IEnumerable<CategoryDto>> GetAllCategoriesWithSubCategories(bool tracking);
        Task<CategoryDto?> FindCategoryWithSubCategories(int categoryId);

        Task<Category?> FindCategoryById(int categoryId,bool tracking);
        Task<IEnumerable<CategoryDto>> FindCategoryByName(string categoryName, bool tracking);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);

    }
}
