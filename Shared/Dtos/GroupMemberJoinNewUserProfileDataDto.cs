using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class GroupMemberJoinNewUserProfileDataDto
    {
        public Guid GroupMemberId { get; set; }
        public Guid UserGroupId { get; set; }
        public Guid UserProfileId { get; set; }
        public int GroupRoleId { get; set; }
        public int MemberStatusId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int AddressId { get; set; }
        public string IdentityUserId { get; set; } = null!;

     
    }
}
