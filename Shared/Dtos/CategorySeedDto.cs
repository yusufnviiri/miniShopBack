using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    
    public class CategorySeedDto
    {
        public int CategoryId { get; set; }
        public int GeneralCategoryId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
