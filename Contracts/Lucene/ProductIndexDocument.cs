using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    /// <summary>
    /// Plain DTO carrying everything needed to build a Lucene document for a product.
    /// Built by the mapper in Services; consumed by the repository in Repository.
    /// No Lucene types — keeps the contract clean.
    /// </summary>
    public sealed class ProductIndexDocument
    {
        // Identity
        public Guid ProductId { get; set; }
        public string Slug { get; set; } = string.Empty;

        // Searchable text
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;

        // Flags
        public bool IsFeatured { get; set; }
        public bool HasImage { get; set; }

        // Categories (IDs for filtering, names for searching/display)
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int SubCategoryCategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string SubCategoryName { get; set; } = string.Empty;
        public string SubCategoryCategoryName { get; set; } = string.Empty;
        public int CommodityClassId { get; set; }

        // Pricing (already converted to smallest currency unit, e.g., UGX)
        public long PriceMinor { get; set; }
        public long OldPriceMinor { get; set; }

        // Time
        public long CreatedAtTicks { get; set; }

        // Display-only
        public Guid ProductImageId { get; set; } = Guid.Empty;

        // Seller (denormalized — Pattern B)
        public Guid SellerProfileId { get; set; }
        public Guid SellerId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string SellerSlug { get; set; } = string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;
        public int SellerTypeId { get; set; }
        public int SellerTierId { get; set; }
        public bool IsVerified { get; set; }
        public string SellerLocation { get; set; } = string.Empty;


        // Aggregates (precomputed at index time)
        public int ReviewCount { get; set; }
        public float AverageRating { get; set; }
    }
}
