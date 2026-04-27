using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class SellerProductDto
    {

        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        // Ownership (DOMAIN, not Identity)
        public int ReviewSummary { get; set; }       
        public Guid  ProductImageId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;


    }
}
