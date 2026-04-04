using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IUserGroupSellerRepo
    {
        Task<IEnumerable<UserGroupSellerDto>> GetAllUserGroupSellers();
        Task<UserGroupSeller?> FindUserGroupSellerById(Guid sellerGroupId, bool tracking);
        void CreateUserGroupSeller(UserGroupSeller userGroupSeller);
        void UpdateUserGroupSeller(UserGroupSeller userGroupSeller);
        void DeleteUserGroupSeller(UserGroupSeller userGroupSeller);

    }
}
