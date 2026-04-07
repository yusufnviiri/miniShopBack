using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ProductDataDto
    {

        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;       
        public decimal Price { get; set; }
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }
        // Ownership (DOMAIN, not Identity)
        public SellerProfile? SellerProfile { get; set; }
        public Guid SellerProfileId { get; set; }
        public Guid SellerUserProfileId { get; set; }
        public int CommodityClassId { get; set; } = 2;
        public ICollection<ShowReviewDto> Reviews { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;      
        public string Category { get; set; } = string.Empty;
        public string? SellerName { get; set; }
        public ICollection<ProductImageRefDto>? ProductImageRefs { get; set; }
        public string Contact { get; set; } = string.Empty;
        public decimal OldPrice { get; set; }
        public bool HasImage { get; set; }
        public bool IsFeatured { get; set; }
        public Guid ProductImageId { get; set; }

        public ICollection<HomePageProductDto> RelatedProducts { get; set; } = [];

    }

}

