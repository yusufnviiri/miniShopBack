using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class AllCategoriesRef
    {
        public IEnumerable<MiniCategoryDto> Categories { get; set; } = [];
        public IEnumerable<MiniSubCategoryDto> SubCategories { get; set; } = [];
        public IEnumerable<MiniSubCategoryCategoryDto> SubCategoryCategories { get; set; }= [];

    }
}
