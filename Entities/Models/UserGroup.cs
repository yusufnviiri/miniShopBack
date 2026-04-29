using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserGroup
    {
        public Guid UserGroupId { get; set; }
        public string UserGroupName { get; set; } = string.Empty;
        public string AboutGroup { get; set; } = string.Empty;
        public GroupType? GroupType { get; set; } // Sacco, Company, Church, NGO
        public int GroupTypeId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Address? Address { get; set; } = null!;
        public int UserStatusId { get; set; } = 1;
        public UserStatus? UserStatus { get; set; }
        public int GroupCategoryId { get; set; } = 2;
        public GroupCategory? GroupCategory { get; set; }
        public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
        public string Contact { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int MaximumSellers { get; set; } = 50;
        public string Slug { get; set; } = string.Empty;
        public int AddressId { get; set; }
        public Guid? SellerProfileId { get; set; }
        public SellerProfile? SellerProfile { get; set; }
        public BuyerProfile? BuyerProfile { get; set; }
        public ICollection<GroupFeaturedProduct>? GroupFeaturedProducts { get; set; } = [];

    }
}
