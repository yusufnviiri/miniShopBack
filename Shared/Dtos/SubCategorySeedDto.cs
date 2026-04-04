using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{  
    public class SubCategorySeedDto
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }        
        public string SubCategoryName { get; set; } = string.Empty;
    }
}
