using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShowSubCategoryDto
    {
        public string SubCategoryName { get; set; }=string.Empty;
        public int SubCategoryId { get; set; }
        public ICollection<CategoryDto> SubCategoryCategories { get; set; } = [];
    }
}
