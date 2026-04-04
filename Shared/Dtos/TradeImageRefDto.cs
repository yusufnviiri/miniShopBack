using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class TradeImageRefDto
    {
        public Guid TradeImageId { get; set; }
        public Guid TradeId { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
