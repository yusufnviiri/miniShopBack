using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ISubCategoryCategoryRepo
    {
        Task<IEnumerable<CategoryDto>> GetAllSubCategoryCategories();
        Task<SubCategoryCategory?> FindSubCategoryCategoryById(int categoryId, bool tracking);
        Task<IEnumerable<CategoryDto>> FindSubCategoryCategoryByName(string categoryName, bool tracking);
        void CreateSubCategoryCategory(SubCategoryCategory category);
        void UpdateSubCategoryCategory(SubCategoryCategory category);
        void DeleteSubCategoryCategory(SubCategoryCategory category);
        Task<IEnumerable<SubCategoryCategoryRefDto>> FindSubCategoryCategoryRefById(int subCategoryId);
        Task<IEnumerable<MiniSubCategoryCategoryDto>> GetAllSubCategoryCategoriesInDb();
        Task<IEnumerable<SubCategoryCategorySeedDto>> GetSubCategoryCAtegorySeedData();

    }
}
