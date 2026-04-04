using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ISubCategoryRepo
    {
        Task<IEnumerable<ShowAllCategoriesDto>> GetAllSubCategories();
        Task<IEnumerable<SubCategorySeedDto>> GetSubCategorySeedData();

        Task<IEnumerable<CategoryDto>> GetAllSubCategoriesWithSubCategories(bool tracking);
        Task<SubCategory?> FindSubCategoryById(int categoryId, bool tracking);
        Task<SubCategory?> FindSubCategoryByIdWithSubCategories(int categoryId);
        Task<IEnumerable<CategoryDto>> FindSubCategoryByName(string categoryName, bool tracking);
        Task<IEnumerable<SubCategoryRefDto>> FindCategorySubCategoryRefById(int categoryId);

        void CreateSubCategory(SubCategory category);
        void UpdateSubCategory(SubCategory category);
        void DeleteSubCategory(SubCategory category);
        Task<IEnumerable<MiniSubCategoryDto>> GetAllSubCategoriesInDb();

    }
}
