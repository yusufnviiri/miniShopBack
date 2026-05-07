using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public sealed class UserCardDto
    {
        public Guid UserProfileId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string SlugName { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsSeller { get; set; }
        public Guid? SellerProfileId { get; set; }
        public string SellerSlugName { get; set; } = string.Empty;
        public int UserGroupCount { get; set; }
        public int ProductCount { get; set; }
        public int TradeCount { get; set; }
        public string Region { get; set; } = string.Empty;

        
        public float Score { get; set; }
    }
}
