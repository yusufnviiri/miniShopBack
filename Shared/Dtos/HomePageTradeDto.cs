using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class HomePageTradeDto
    {
        public Guid TradeId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string TradeName { get; set; } = null!;
        public string Category {  get; set; } = string.Empty;
        public bool IsFeatured { get; set; }
        public Guid SellerProfileId { get; set; }   
        public Guid TradeImageId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string TradeSlugName { get; set; } = string.Empty;
        public string SellerSlugName { get; set; } = string.Empty;

    }
}
