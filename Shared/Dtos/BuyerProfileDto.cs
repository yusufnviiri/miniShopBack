using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class BuyerProfileDto
    {
        public Guid BuyerProfileId { get; set; }
        public required Guid BuyerId { get; set; }
        public int PurchaseLimit { get; set; }
        public int BuyerTierId { get; set; }
        public BuyerTier? BuyerTier { get; set; }
        public int BuyerTypeId { get; set; }
        public BuyerType? BuyerType { get; set; }
    }
}
