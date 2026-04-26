using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ProductCategoryData
    {
        public ICollection<CategoryRefDto> Categories { get; set; } = [];
        public ICollection<SubCategoryRefDto> SubCategories { get; set; } = [];
        public ICollection<SubCategoryCategoryRefDto> SubCategoryCategories { get; set; } = [];
    }
}
