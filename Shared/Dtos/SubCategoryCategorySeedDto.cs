using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{  
    public class SubCategoryCategorySeedDto
    {
        public int SubCategoryCategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryCategoryName { get; set; } = string.Empty;
    }
}
