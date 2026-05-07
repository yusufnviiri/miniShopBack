using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    /// <summary>
    /// Card shape for trade results, mirroring the search-time fields you specified.
    /// Used everywhere a trade card renders: search results, listing pages, etc.
    /// </summary>
    public sealed class TradeCardDto
    {
        public Guid TradeId { get; set; }
        public string TradeName { get; set; } = string.Empty;
        public string SlugName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public bool IsFeatured { get; set; }
        public Guid TradeImageId { get; set; }

        // Categories
        public string CategoryName { get; set; } = string.Empty;
        public string SubCategoryName { get; set; } = string.Empty;
        public string SubCategoryCategoryName { get; set; } = string.Empty;

        // Seller
        public Guid SellerProfileId { get; set; }
        public Guid SellerId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string SellerSlugName { get; set; } = string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;
        public int SellerTypeId { get; set; }
        public int SellerTierId { get; set; }
        public bool IsVerified { get; set; }

        // Aggregates
        public int ReviewCount { get; set; }
        public float AverageRating { get; set; }

        public float Score { get; set; }
    }
}
