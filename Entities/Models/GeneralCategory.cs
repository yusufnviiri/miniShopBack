using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class GeneralCategory
    {
        public int GeneralCategoryId { get; set; }
        public string GeneralCategoryName { get; set; } = string.Empty;
        public IEnumerable<Category> Categories { get; set; } = [];

    }
}
