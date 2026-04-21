using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class TradeDataDto
    {
        public Guid TradeId { get; set; }
        public string TradeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }
        public string Contact { get; set; } = string.Empty;
        public Guid SellerUserProfileId { get; set; }
        public string WhatsAppNumber { get; set; } = string.Empty;




        public Guid SellerProfileId { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<TradeImageRefDto> TradeImageRefDtos  { get; set; } = [];
        public ICollection<ShowReviewDto> Reviews { get; set; } = [];
        public ICollection<HomePageTradeDto> RelatedTrades { get; set; } = [];
    }
}
