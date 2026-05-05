using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class UserProfileDto
    {
        public Guid UserProfileId { get; set; }
     
        public ICollection<ShowUserGroupDto> UserGroups { get; set; }  = [];
        public ShowApplicationUserDto? ApplicationUser { get; set; }
        public Address?Address { get;set; }
        public int AddressId { get; set; }
        public bool IsSeller { get; set; }= false;
        public IEnumerable<UserPreferenceDto> UserPreferences { get; set; } = [];
        public IEnumerable<UserFollowerDto> Following { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string SlugName { get; set; } = string.Empty;
        public string SellerSlugName { get; set; } = string.Empty;



    }
}
