using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class SellerTradeDto
    {
        public Guid TradeId { get; set; }
        public string TradeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int ReviewSummary { get; set; }
        public Guid TradeImageId { get; set; }
        public bool IsFeatured { get; set; }    
        public string CategoryName { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public string SellerSlugName { get; set; } = string.Empty;
        public string TradeSlugName { get; set; } = string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;

        




    }
}
