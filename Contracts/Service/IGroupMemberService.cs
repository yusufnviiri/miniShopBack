using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IGroupMemberService
    {
        Task<IEnumerable<ShowGroupMemberDto>> GetAllGroupMembersAsync();
        Task<ShowGroupMemberDto?> GetGroupMemberByIdAsync(Guid userGroupId, Guid userProfileId, bool tracking);
        Task<GroupMember?> FindGroupMemberByIdAsync(Guid memberID, bool tracking);
        Task<string> CreateGroupMemberAsync(GroupMemberDto groupMember, CancellationToken ct = default);
        Task UpdateGroupMemberAsync(GroupMemberDto groupMember );
        Task UpdateGroupMemberProfileAsync(ShowGroupMemberDto groupMember);
        Task DeleteGroupMemberAsync(Guid memberID );
        Task<IEnumerable<GroupMemberRolesDto>> GetAllMemberRolesAsync(Guid userProfileId);
        Task<GroupMember?> FindGroupMemberByUserProfileIdAsync(Guid userProfileId);


    }
}
