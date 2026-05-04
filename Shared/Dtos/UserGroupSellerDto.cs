using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class UserGroupSellerDto
    {
        public Guid UserGroupSellerId { get; set; }
        public Guid SellerProfileId { get; set; }
        public SellerProfile? SellerProfile { get; set; }
        public Guid UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }
        public string SellerSlugName { get; set; } = string.Empty;
        public string UserGroupSlugName { get; set; } = string.Empty;
    }
}
