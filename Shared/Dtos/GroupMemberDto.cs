using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class GroupMemberDto
    {
        public Guid GroupMemberId { get; set; }
        public Guid UserGroupId { get; set; }
        public Guid UserProfileId { get; set; }
        public int GroupRoleId { get; set; }
        public int MemberStatusId { get; set; }
  
    }
}
