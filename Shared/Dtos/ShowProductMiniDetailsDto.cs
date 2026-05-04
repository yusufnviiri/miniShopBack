using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShowProductMiniDetailsDto
    {

        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }
        public bool IsFeatured { get; set; }
        public string Condition { get; set; } = string.Empty;


        // Ownership (DOMAIN, not Identity)
        public Guid SellerProfileId { get; set; }
        public int CommodityClassId { get; set; } = 2;
        public int  ReviewSummary { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Description { get; set; } = default!;
        public ICollection<ProductImageRefDto>? ProductImageRefs { get; set; }
        public string? SellerName { get; set; }
        public bool HasImage { get; set; }
        public Guid ProductImageId { get; set; }
        public string SlugName { get; set; } = string.Empty;
        public string SellerSlugName { get; set; } = string.Empty;






    }
}
