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
    public class GroupFeaturedProductRepo : RepositoryBase<GroupFeaturedProduct>, IGroupFeaturedProductRepo
    {
        public GroupFeaturedProductRepo(ApplicationDbContext _db) : base(_db)
        {

        }


        public async Task<IEnumerable<GroupFeaturedProductDto>> GetAllGroupFeaturedProducts() => await FindAll(false).Select(p => new GroupFeaturedProductDto() { GroupFeaturedProductId = p.GroupFeaturedProductId, ProductId = p.ProductId, UserGroupId = p.UserGroupId, ProductName = p.Product != null ? p.Product.ProductName : "", UserGroupName = p.UserGroup != null ? p.UserGroup.UserGroupName : "" }).ToListAsync();
        public IQueryable<GroupFeaturedProduct> GroupFeaturedProductsQueryData() => FindAll(false);

        //Task<IEnumerable<GroupFeaturedProductDto>> GetAllGroupFeaturedProductDtos();
        public async Task<GroupFeaturedProduct?> FindGroupFeaturedProductById(int groupFeaturedProductId, bool tracking) => await FindByCondition(p => p.GroupFeaturedProductId == groupFeaturedProductId, tracking).FirstOrDefaultAsync();
        public async Task<GroupFeaturedProduct?> FindGroupFeaturedProductForUpdate(int groupFeaturedProductId) => await FindByCondition(p => p.GroupFeaturedProductId == groupFeaturedProductId, true).FirstOrDefaultAsync();
        public void CreateGroupFeaturedProduct(GroupFeaturedProduct groupFeaturedProduct) => CreateBase(groupFeaturedProduct);
        public void UpdateGroupFeaturedProduct(GroupFeaturedProduct groupFeaturedProduct) => UpdateBase(groupFeaturedProduct);
        public void DeleteGroupFeaturedProduct(GroupFeaturedProduct groupFeaturedProduct) => DeleteBase(groupFeaturedProduct);

        public async Task<bool> IsGroupFeaturedProduct(Guid productId, Guid userGroupId)
        {
            return await FindByCondition(p => p.ProductId == productId && p.UserGroupId == userGroupId, false).AnyAsync();
        }
        public IQueryable<GroupFeaturedProduct> GroupFeaturedProductsQuery(bool tracking)
        {
            return FindAll(tracking);

        }

        public void DeleteGroupFeaturedProducts(ICollection<GroupFeaturedProduct> featuredProducts) => DeleteRange(featuredProducts);

    }
}