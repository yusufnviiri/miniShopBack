using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShowAllCategoriesDto
    {
        public IEnumerable<ShowSubCategoryDto> SubCategories { get; set; } = [];
        public string CategoryName { get; set; }=string.Empty;
        public int RefId { get; set; }
        public int CategoryId { get; set; }


    }
}
