using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class SellerProfileDto
    {
        public Guid SellerProfileId { get; set; }
        public Guid SellerId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public SellerOffering? SellerOffering { get; set; }
        public int SellerOfferingId { get; set; }
        public string WhatsAppNumber { get; set; } = string.Empty;


        public SellerType? SellerType { get; set; }
        public int SellerTypeId { get; set; }
        public int SellerPolicyId { get; set; } 
        public SellerPolicy? SellerPolicy { get; set; }
        public int SellerTierId { get; set; } = 1; //default tier
        public ICollection<SellerRestriction>? SellerRestrictions { get; set; } = [];
    }
}
