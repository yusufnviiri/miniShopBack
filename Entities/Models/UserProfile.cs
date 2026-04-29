using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserProfile
    {
        public Guid UserProfileId { get; set; }
        public string IdentityUserId { get; set; } = null!;
        public ApplicationUser IdentityUser { get; set; } =null!;
        public int AddressId { get; set; }
        public Guid? ActiveGroupId { get; set; }  //nullable
        public Address? Address { get; set; }
        public ICollection<GroupMember> GroupMemberships { get; set; }
            = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserStatusId { get; set; } = 1;
        public UserStatus? UserStatus { get; set; }
        public string Slug { get; set; } = string.Empty;
        public BuyerProfile? BuyerProfile { get; set; }
        public Guid? BuyerProfileId { get; set; }
        public Guid? SellerProfileId { get; set; }
        public SellerProfile? SellerProfile { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
        public ICollection<TradeReview> TradeReviews { get; set; } = new List<TradeReview>();


    }

}
