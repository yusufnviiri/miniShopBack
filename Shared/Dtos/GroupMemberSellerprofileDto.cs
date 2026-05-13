using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class GroupMemberSellerprofileDto
    {

        public Guid SellerProfileId { get; set; }
        public required Guid SellerId { get; set; }
        public required Guid UserGroupId { get; set; }

        public string SellerName { get; set; } = string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string SellerLocation { get; set; } = string.Empty;
     
        public int SellerOfferingId { get; set; } = 1;
        public int SellerTypeId { get; set; } = 1;
        public string Slug { get; set; } = string.Empty;
        public int MaximumAllowedItems { get; set; } = 10;
        public int SellerPolicyId { get; set; } = 2; //default policy
        public SellerPolicy? SellerPolicy { get; set; }
        public int SellerTierId { get; set; } = 1; //default tier
      
        public bool IsVerified { get; set; } = true;
    }
}
