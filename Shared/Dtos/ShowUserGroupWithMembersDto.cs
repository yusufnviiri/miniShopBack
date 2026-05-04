using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShowuserGroupWithMembersDto
    {
        public Guid UserGroupId { get; set; }
        public string UserGroupName { get; set; } = string.Empty;
        public string UserGroupSlugName { get; set; } = string.Empty;

        public string AboutGroup { get; set; } = string.Empty;

        public string? City { get; set; } = string.Empty;
        public string? Region { get; set; } = string.Empty;
        public string Country { get; set; }= string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string? Company { get; set; } = default!;
        public int MemberCount { get; set; }
        public string GroupType {  get; set; } = string.Empty;
        public ICollection<ShowGroupMemberDto>? Members { get; set; } = [];
    }

}
