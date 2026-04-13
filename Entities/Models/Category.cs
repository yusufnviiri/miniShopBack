using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int GeneralCategoryId { get; set; }
        public GeneralCategory? GeneralCategory { get; set; }
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<Trade> Trades { get; set; } = [];
        public string Type { get; set; } = string.Empty;

        public ICollection<SubCategory> SubCategories { get; set; } = [];
        public ICollection<UserPreferenceCategory> UserPreferenceCategories { get; set; } = [];


    }
}
