using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class SubCategoryTreeDto
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = string.Empty;
        public ICollection<SubCategoryCategoryRefDto> SubCategoryCategories { get; set; } = [];
    }
}
