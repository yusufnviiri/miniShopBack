using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IUserGroupRepo
    {
        Task<IEnumerable<ShowUserGroupDto>> GetUserGroups();

        Task<ShowUserGroupDto?> GetUserGroupById(Guid userGroupId);

        Task<int> GetGroupAddress(Guid userGroupId);
       Task< string?> GetGroupGroupName(Guid userGroupId);


        Task<bool> IsGroupMember(Guid userProfileId, Guid userGroupId);


        Task<ShowuserGroupWithMembersDto?> GetUserGroupWithMembers(Guid userGroupId);
        Task<UserGroup?> FindUserGroupById(Guid userGroupId, bool tracking);
        Guid CreateUserGroup(UserGroup userGroup );
        void UpdateUserGroup(UserGroup userGroup);
        void DeleteUserGroup(UserGroup userGroup);
    }
}
