using Entities.Models;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IUserGroupService
    {


        Task<(ICollection<ShowUserGroupDto> groupsData, MetaData MetaData)> GetUserGroupsAsync(GeneralRequestParameters requestParameters, CancellationToken cancellationToken);

        Task<ShowUserGroupDto?> GetUserGroupByIdAsync(Guid userGroupId);

        Task<ShowuserGroupWithMembersDto?> GetUserGroupWithMembersAsync(Guid userGroupId);
        Task<ShowuserGroupWithMembersDto?> GetUserGroupWithMembersWithSlugAsync(string slug );

        Task<UserGroup?> FindUserGroupByIdAsync(Guid userGroupId, bool tracking);
        Task CreateUserGroupAsync(NewUserGroupDto userGroup, CancellationToken ct = default);
        Task UpdateUserGroupAsync(NewUserGroupDto userGroup, CancellationToken ct = default);
        Task DeleteUserGroupAsync(Guid userGroupId);
        Task ToggleUserGroupIsPinnedStateAsync(Guid userGroupId, CancellationToken ct = default);

    }
}
