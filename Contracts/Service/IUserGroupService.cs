using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IUserGroupService
    {
        Task<IEnumerable<ShowUserGroupDto>> GetUserGroupsAsync();
        Task<ShowUserGroupDto> GetUserGroupByIdAsync(Guid userGroupId);

        Task<ShowuserGroupWithMembersDto?> GetUserGroupWithMembersAsync(Guid userGroupId);
        Task<ShowuserGroupWithMembersDto?> GetUserGroupWithMembersWithSlugAsync(string slug );

        Task<UserGroup?> FindUserGroupByIdAsync(Guid userGroupId, bool tracking);
        Task CreateUserGroupAsync(NewUserGroupDto userGroup);
        Task UpdateUserGroupAsync(NewUserGroupDto userGroup);
        Task DeleteUserGroupAsync(Guid userGroupId);
    }
}
