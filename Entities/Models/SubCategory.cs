using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }=string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<Trade> Trades { get; set; } = [];


        public ICollection<SubCategoryCategory> SubCategoryCategories { get; set; } = [];

    }
}
