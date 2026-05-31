using Entities.Models;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IUserGroupRepo
    {
        Task<PagedList<ShowUserGroupDto>> GetUserGroups(GeneralRequestParameters parameters , CancellationToken cancellationToken);

        Task<ShowUserGroupDto?> GetUserGroupById(Guid userGroupId);

        Task<int> GetGroupAddress(Guid userGroupId);
       Task< string?> GetGroupGroupName(Guid userGroupId);
        Task<string?> GetGroupGroupSlugName(Guid userGroupId);

        Task<string> GetGroupSlugNameOnly(Guid userGroupId);


        Task ToggleUserGroupIsPinnedState(Guid userGroupId);





        Task<bool> IsGroupMember(Guid userProfileId, Guid userGroupId);


        Task<ShowuserGroupWithMembersDto?> GetUserGroupWithMembers(Guid userGroupId);
        Task<UserGroup?> FindUserGroupById(Guid userGroupId, bool tracking);
        Guid CreateUserGroup(UserGroup userGroup );
        void UpdateUserGroup(UserGroup userGroup);
        void DeleteUserGroup(UserGroup userGroup);
        Task<int> NumberOfUserGroups();
        Task<long> NextUserGroupSlugNumberAsync();

        Task<Guid> GetUserGroupIdBySlugName(string slug);
 
    }
}
