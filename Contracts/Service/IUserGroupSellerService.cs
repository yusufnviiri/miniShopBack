using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IUserGroupSellerService
    {
        Task<IEnumerable<UserGroupSellerDto>> GetAllUserGroupSellersAsync();
        Task<UserGroupSeller?> FindUserGroupSellerByIdAsync(Guid sellerGroupId, bool tracking);
        Task CreateUserGroupSellerAsync(UserGroupSellerDto userGroupSeller);
        Task UpdateUserGroupSellerAsync(UserGroupSellerDto userGroupSeller);
        Task DeleteUserGroupSellerAsync(Guid userGroupSellerId);
    }
}
