using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class GroupProductsAndTradesList
    {
        public IEnumerable<SellerProductDto> GroupProducts { get; set; } = [];
        public IEnumerable<SellerTradeDto> GroupTrades { get; set; } = [];


    }
}
