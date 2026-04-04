using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class GroupMember
    {
        public Guid GroupMemberId { get; set; }
        public Guid UserGroupId { get; set; }
        public Guid UserProfileId { get; set; }
        public GroupRole? GroupRole { get; set; }
        public int GroupRoleId {  get; set; }
        public int MemberStatusId  { get; set; }
        public MemberStatus? MemberStatus { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public UserGroup Group { get; set; } = null!;
        public UserProfile? UserProfile { get; set; }
        public ICollection<GroupSeller>? GroupSellers { get; set; } = [];
    }

}
