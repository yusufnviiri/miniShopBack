using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class CategoriesSubCategoryCategoryDto
    {
        public ICollection<CategorySeedDto> Categories { get; set; } = [];
        public ICollection<SubCategorySeedDto> SubCategories  { get; set; } = [];
        public ICollection<SubCategoryCategorySeedDto> SubCategoryCategories  { get; set; } = [];

    }
}
