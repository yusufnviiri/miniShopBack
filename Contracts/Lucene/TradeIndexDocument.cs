using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    /// <summary>
    /// Plain DTO carrying everything needed to build a Lucene document for a trade.
    /// Built by the mapper in Services; consumed by the repository in Repository.
    /// </summary>
    public sealed class TradeIndexDocument
    {
        // Identity
        public Guid TradeId { get; set; }
        public string Slug { get; set; } = string.Empty;

        // Searchable text
        public string TradeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Categories (IDs for filtering, names for searching/display)
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int SubCategoryCategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string SubCategoryName { get; set; } = string.Empty;
        public string SubCategoryCategoryName { get; set; } = string.Empty;
        public int CommodityClassId { get; set; }

        // Time (for sort by newest)
        public long CreatedAtTicks { get; set; }

        // Display-only image reference
        public Guid TradeImageId { get; set; }

        // Flags
        public bool IsFeatured { get; set; }
        public bool HasImage { get; set; }
        public bool IsModified { get; set; }

        // Seller (denormalized — Pattern B)
        public Guid SellerProfileId { get; set; }
        public Guid SellerId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string SellerSlugName { get; set; } = string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;
        public int SellerTypeId { get; set; }
        public int SellerTierId { get; set; }
        public bool IsVerified { get; set; }

        // Aggregates (precomputed at index time)
        public int ReviewCount { get; set; }
        public float AverageRating { get; set; }
    }
}
