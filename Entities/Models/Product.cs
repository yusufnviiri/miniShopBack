using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int SubCategoryCategoryId { get; set; }
        public Category? Category { get; set; }
        public SubCategory? SubCategory { get; set; }
        public SubCategoryCategory? SubCategoryCategory { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public bool IsFeatured { get; set; }
        public bool HasImage { get; set; }
        public string Description { get; set; } = string.Empty;

        // Ownership (DOMAIN, not Identity)
        public SellerProfile? SellerProfile { get; set; }
        public  Guid SellerProfileId { get; set; }
        public ICollection<ProductImage> Images { get; set; } = [];
        public int CommodityClassId { get; set; } = 2;
        public ICollection<Review> Reviews { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 
    }
}
