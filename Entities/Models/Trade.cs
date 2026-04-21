using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
        public class Trade
    {
        public Guid TradeId { get; set; }
        public string TradeName { get; set; } = null!;
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int SubCategoryCategoryId { get; set; }
        public Category? Category { get; set; }
        public SubCategory? SubCategory { get; set; }
        public SubCategoryCategory? SubCategoryCategory { get; set; }
        public string Description { get; set; } = default!;

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public bool HasImage { get; set; }
        public bool IsModified { get; set; }
        public bool IsFeatured { get; set; }

        // Ownership (DOMAIN, not Identity)
        public SellerProfile? SellerProfile { get; set; }
        public Guid SellerProfileId { get; set; }
        public ICollection<TradeImage> Images { get; set; } = [];
        public int CommodityClassId { get; set; } = 2;
        public ICollection<TradeReview> TradeReviews { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<TradeBooking> TradeBookings  { get; set; } = [];
        public ICollection<TradeImpression> TradeImpressions { get; set; } = [];
    }

}

