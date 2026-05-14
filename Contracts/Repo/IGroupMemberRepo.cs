using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IGroupMemberRepo
    {
        Task<IEnumerable<ShowGroupMemberDto>> GetAllGroupMembers();
        Task<IEnumerable<GroupMemberRolesDto>> GetAllMemberRoles(Guid userProfileId);

        Task<ShowGroupMemberDto?> GetGroupMemberById(Guid userGroupId, Guid userProfileId, bool tracking);
        Task<GroupMember?> FindGroupMemberById(Guid memberId, bool tracking);
        Task<GroupMember?> FindGroupMemberByUserProfileId(Guid userProfileId);
        Task<GroupMember?> FindGroupMemberByUserProfileIdWithTracking(Guid userProfileId, Guid userGroupId, bool tracking);
        Task<bool> IsUserInGroup(Guid userProfileId, Guid userGroupId);
        void CreateGroupMember(GroupMember groupMember);
        void UpdateGroupMember(GroupMember groupMember);
        void DeleteGroupMember(GroupMember groupMember);
        Task<IReadOnlyList<Guid>> GetGroupMemberProfileIds(Guid groupId);

    }
}
