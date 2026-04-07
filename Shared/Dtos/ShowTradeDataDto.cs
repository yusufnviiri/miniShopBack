using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShowTradeDataDto
    {

        public Guid TradeId { get; set; }
        public string TradeName { get; set; } = null!;
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int SubCategoryCategoryId { get; set; }
        public string Description { get; set; } = default!;
        public bool IsFeatured { get; set; }      
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public bool HasImage { get; set; }
        public bool IsModified { get; set; }

        // Ownership (DOMAIN, not Identity)
        public Guid SellerProfileId { get; set; }
        public ICollection<ShowReviewDto> Reviews { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Category { get; set; } = string.Empty;
        public string? SellerName { get; set; }
        public ICollection<TradeImageRefDto>? TradeImageRefs { get; set; }
        public string Contact { get; set; } = string.Empty;
        public ICollection<TradeBooking> TradeBookings { get; set; } = [];

    }
}
