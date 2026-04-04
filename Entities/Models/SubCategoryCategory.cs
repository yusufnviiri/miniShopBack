using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SubCategoryCategory
    {
        public int SubCategoryCategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<Trade> Trades { get; set; } = [];
        public string SubCategoryCategoryName { get; set; } = string.Empty;
    }
}
