using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class UserGroupSellerRepo:RepositoryBase<UserGroupSeller>, IUserGroupSellerRepo
    {
        public UserGroupSellerRepo(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

       public async Task<IEnumerable<UserGroupSellerDto>> GetAllUserGroupSellers()
        {
            return await FindAll(false)
                .Select(ugs => new UserGroupSellerDto
                {
                    UserGroupSellerId = ugs.UserGroupSellerId,
                    SellerProfileId = ugs.SellerProfileId,
                    SellerProfile = ugs.SellerProfile,
                    UserGroupId = ugs.UserGroupId,
                    UserGroup = ugs.UserGroup
                }).ToListAsync();
                
        }
        public async Task<UserGroupSeller?> FindUserGroupSellerById(Guid sellerGroupId, bool tracking)
        {
            return await FindByCondition(ugs => ugs.UserGroupSellerId.Equals(sellerGroupId), tracking)
                .FirstOrDefaultAsync();
        }
        public void CreateUserGroupSeller(UserGroupSeller userGroupSeller)=>CreateBase(userGroupSeller);
        public void UpdateUserGroupSeller(UserGroupSeller userGroupSeller)=>UpdateBase(userGroupSeller);
        public void DeleteUserGroupSeller(UserGroupSeller userGroupSeller)=>DeleteBase(userGroupSeller);
    }
}
