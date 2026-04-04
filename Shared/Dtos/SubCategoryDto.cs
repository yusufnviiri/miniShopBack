using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class SubCategoryDto
    {
        public string SubCategoryName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

    }
}
