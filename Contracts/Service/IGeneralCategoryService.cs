using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IGeneralCategoryService
    {



        Task<IEnumerable<CategoryRefDto>> GetAllGeneralCategoriesAsync();
        Task<IEnumerable<CategoryDto>> GetReferencedGeneralCategoriesAsync();

        Task<IEnumerable<CategoryDto>> GetAllGeneralCategoriesWithCategoriesAsync(bool tracking);
        Task<CategoryDto?> FindCategoryWithSubCategoriesAsync(int generalCategoryId);
        Task<GeneralCategory?> FindGeneralCategoryByIdAsync(int generalCategoryId, bool tracking);
        Task<IEnumerable<CategoryDto>> FindGeneralCategoryByNameAsync(string generalCategoryName, bool tracking);
        Task CreateGeneralCategoryAsync(GeneralCategory generalCategory);
        Task UpdateGeneralCategoryAsync(GeneralCategory generalCategory);
        Task AddCategoryToGeneralCategoryAsync(GeneralCategoryUpdateDto generalCategory);

        Task DeleteGeneralCategoryAsync(int generalCategoryId);
        Task<IEnumerable<CategoryDto>> GetReferencedProductGeneralCategoriesAsync();
        Task<IEnumerable<CategoryDto>> GetReferencedTradeGeneralCategoriesAsync();
    }
}
