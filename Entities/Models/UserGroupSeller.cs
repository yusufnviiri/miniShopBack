using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserGroupSeller
    {
        public Guid UserGroupSellerId { get; set; }
        public Guid SellerProfileId { get; set; }        
        public SellerProfile? SellerProfile { get; set; }
        public Guid UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }
    }
}
