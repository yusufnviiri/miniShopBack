using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShowProductDto
    {

        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int SubCategoryCategoryId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }
        public string MeasurementUnit { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }

        // Ownership (DOMAIN, not Identity)
        public SellerProfile? SellerProfile { get; set; }
        public Guid SellerProfileId { get; set; }
        public Guid SellerUserProfileId { get; set; }
        public ICollection<ProductImage> Images { get; set; } = [];
        public int CommodityClassId { get; set; } = 2;
        public ICollection<ShowReviewDto> Reviews { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<ProductAttributeValueDto> ProductAttributeValues { get; set; } = [];    
        public string Category { get; set; } = string.Empty;
        public string? SellerName { get; set; }
        public ICollection<ProductImageRefDto>? ProductImageRefs  { get; set; }
        public string Contact { get; set; } = string.Empty;
        public decimal OldPrice { get; set; }
        public bool HasImage { get; set; }



    }
}
