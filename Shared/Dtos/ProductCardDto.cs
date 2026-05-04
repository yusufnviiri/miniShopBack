using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public sealed class ProductCardDto
    {
        // Product
        public Guid ProductId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }

        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public string PrimaryImageUrl { get; set; } = string.Empty;

        // Categories (display)
        public string CategoryName { get; set; } = string.Empty;
        public string SubCategoryName { get; set; } = string.Empty;
        public string SubCategoryCategoryName { get; set; } = string.Empty;

        // Seller
        public Guid SellerProfileId { get; set; }
        public Guid SellerId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string SellerSlug { get; set; } = string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;
        public int SellerTypeId { get; set; }
        public int SellerTierId { get; set; }
        public bool IsVerified { get; set; }

        // Aggregates
        public int ReviewCount { get; set; }
        public float AverageRating { get; set; }

        /// <summary>Lucene relevance score (useful for debugging).</summary>
        public float Score { get; set; }
    }
}
