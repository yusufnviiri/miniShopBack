using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IGeneralCategoryRepo
    {

        Task<IEnumerable<CategoryRefDto>> GetAllGeneralCategories();
        Task<IEnumerable<CategoryDto>> GetReferencedGeneralCategories();
        Task<IEnumerable<CategoryDto>> GetReferencedProductGeneralCategories();
        Task<IEnumerable<CategoryDto>> GetReferencedTradeGeneralCategories();
        Task<IEnumerable<CategoryDto>> GetAllGeneralCategoriesWithCategories(bool tracking);
        Task<CategoryDto?> FindCategoryWithSubCategories(int generalCategoryId);
        Task<GeneralCategory?> FindGeneralCategoryById(int generalCategoryId, bool tracking);
        Task<GeneralCategory?> FindGeneralCategoryWithCategoriesById(int generalCategoryId, bool tracking);
        Task<IEnumerable<CategoryDto>> FindGeneralCategoryByName(string generalCategoryName, bool tracking);
         void CreateGeneralCategory(GeneralCategory generalCategory );
        void UpdateGeneralCategory(GeneralCategory generalCategory);
        void DeleteGeneralCategory(GeneralCategory generalCategory);
    }
}
