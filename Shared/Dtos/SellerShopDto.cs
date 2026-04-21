using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class SellerShopDto
    {
        public Guid SellerProfileId { get; set; }
        public Guid SellerId { get; set; }
        public string SellerName { get; set; }= string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string SellerTypeDescription { get; set; }= string.Empty;
        public ICollection<SellerGroupDto> Groups { get; set; } = [];
        public ICollection<SellerProductDto> Products { get; set; } = [];
        public ICollection<SellerTradeDto> Trades { get; set; } = [];
        public IEnumerable<UserFollowerDto> Followers { get; set; } = [];


    }
}
