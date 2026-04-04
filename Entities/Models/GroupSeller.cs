using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class GroupSeller
    {
        public Guid GroupSellerId { get; set; }
        public Guid UserGroupId { get; set; }
        public Guid SellerProfileId { get; set; }
        public SellerProfile? SellerProfile { get; set; }
        public GroupMember? Member { get; set; }
        public Guid GroupMemberId { get; set; }
    }
}
