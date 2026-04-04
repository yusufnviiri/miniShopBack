using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class GroupSellerRepo : RepositoryBase<GroupSeller>, IGroupSellerRepo
    {
        public GroupSellerRepo(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

       public async Task<IEnumerable<GroupSeller>> GetAllGroupSellers()
        {
            var groupSellers = await FindAll(false).ToListAsync();
            return groupSellers;
        }
        public async Task<GroupSeller?> FindGroupSellerById(Guid groupSellerId, bool tracking)
        {
            var seller = await FindByCondition(p => p.GroupSellerId == groupSellerId, tracking).FirstOrDefaultAsync();
            return seller;
        }
        public void CreateGroupSeller(GroupSeller groupSeller)=>CreateBase(groupSeller);
        public void UpdateGroupSeller(GroupSeller groupSeller)=>UpdateBase(groupSeller);
        public void DeleteGroupSeller(GroupSeller groupSeller)=>DeleteBase(groupSeller);

    }
}
