using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class GroupShopDto
    {
        public MiniGroupDetailsDto GroupDetails { get; set; } = new MiniGroupDetailsDto();     
        public GroupProductsAndTradesList? GroupProducts { get; set; } 
       public ICollection<SellerProductsListDto> MemberProducts { get; set; } = [];
        public ICollection<SellerTradeListDto> MemberTrades { get; set; } = [];

    }
}
