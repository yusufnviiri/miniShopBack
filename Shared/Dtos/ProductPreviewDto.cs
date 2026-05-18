using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public sealed class ProductPreviewDto
    {
        public Guid ProductId { get; set; }
        public string Slug { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? SellerName { get; set; }
        public string? PrimaryImageId { get; set; }
    }
}
