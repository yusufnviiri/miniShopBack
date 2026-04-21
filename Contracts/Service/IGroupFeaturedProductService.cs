using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IGroupFeaturedProductService
    {
        Task<IEnumerable<GroupFeaturedProductDto>> GetAllGroupFeaturedProductsAsync();
        Task<IEnumerable<GroupFeaturedProductDto>> GetGroupFeaturedProductsByUserGroupIdAsync(Guid userGroupId);
        //Task<IEnumerable<GroupFeaturedProductDto>> GetAllGroupFeaturedProductDtos();
        Task<GroupFeaturedProduct?> FindGroupFeaturedProductByIdAsync(int groupFeaturedProductId, bool tracking);
        Task CreateGroupFeaturedProductAsync(NewGroupFeaturedProductDto groupFeaturedProduct);
        Task UpdateGroupFeaturedProductAsync(GroupFeaturedProduct groupFeaturedProduct);
        Task DeleteGroupFeaturedProductAsync(int groupFeaturedProductId);
    }
}
