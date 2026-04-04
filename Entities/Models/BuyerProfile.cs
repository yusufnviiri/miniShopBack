using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BuyerProfile
    {
        public Guid BuyerProfileId { get; set; }
        public Guid BuyerId { get; set; }
        public int PurchaseLimit { get; set; }
        public int BuyerTierId { get; set; }
        public BuyerTier? BuyerTier { get; set; }
        public int BuyerTypeId { get; set; }
        public BuyerType? BuyerType { get; set; }
        public UserProfile? UserProfile { get; set; }
        public Guid? UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }
    }
}
