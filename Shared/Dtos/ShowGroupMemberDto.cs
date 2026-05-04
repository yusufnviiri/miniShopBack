using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShowGroupMemberDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string GroupRole { get; set; } = default!;
        public string MemberStatus { get; set; } = default!;
        public DateTime JoinedAt { get; set; } 
        public string GroupName { get; set; } = null!;
        public Guid GroupMemberId { get; set; }
        public int GroupTypeId { get; set; }
        public Guid UserGroupId { get; set; }
        public Guid UserProfileId { get; set; }
        public string IdentityUserId { get; set; } = null!;
        public int GroupRoleId { get; set; }
        public int MemberStatusId { get; set; }
        public bool IsSeller { get; set; }
        public string SlugName { get; set; } = string.Empty;



    }
}
