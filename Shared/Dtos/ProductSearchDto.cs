using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ProductSearchDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string Description { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public string SubCategoryName { get; set; } = "";
        public decimal Price { get; set; }
        public double AverageRating { get; set; }
        public int TotalProductImpressions { get; set; }  
        public DateTime CreatedAt { get; set; }
    }
}
