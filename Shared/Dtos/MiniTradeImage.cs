using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class MiniTradeImage
    {
        public Guid ImageId { get; set; }
        public Guid TradeId { get; set; } = Guid.Empty;

    }
}
