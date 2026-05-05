using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ProductCardDto
    {
        // ── Product ──────────────────────────────────────────────
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SlugName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }

        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }

        /// <summary>
        /// GUID of the primary product image. Frontend builds the URL
        /// from this ID using the existing image-serving endpoint.
        /// </summary>
        public Guid ProductImageId { get; set; }

        // ── Categories (display) ─────────────────────────────────
        public string CategoryName { get; set; } = string.Empty;
        public string SubCategoryName { get; set; } = string.Empty;
        public string SubCategoryCategoryName { get; set; } = string.Empty;

        // ── Seller ───────────────────────────────────────────────
        public Guid SellerProfileId { get; set; }
        public Guid SellerId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string SellerSlugName { get; set; } = string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;
        public int SellerTypeId { get; set; }
        public int SellerTierId { get; set; }
        public bool IsVerified { get; set; }

        // ── Aggregates (search-relevant; homepage may ignore) ────
        public int ReviewCount { get; set; }
        public float AverageRating { get; set; }

        /// <summary>
        /// Lucene relevance score — only meaningful for search results.
        /// Defaults to 0 when populated from non-Lucene sources (homepage, etc.).
        /// </summary>
        public float Score { get; set; }
    }
}
