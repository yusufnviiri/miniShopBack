using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IGroupFeaturedProductRepo
    {
        Task<IEnumerable<GroupFeaturedProductDto>> GetAllGroupFeaturedProducts();
        Task<bool> IsGroupFeaturedProduct(Guid productId,Guid userGroupId);

        IQueryable<GroupFeaturedProduct> GroupFeaturedProductsQueryData();

        //Task<IEnumerable<GroupFeaturedProductDto>> GetAllGroupFeaturedProductDtos();
        Task<GroupFeaturedProduct?> FindGroupFeaturedProductById(int groupFeaturedProductId, bool tracking);
        Task<GroupFeaturedProduct?> FindGroupFeaturedProductForUpdate(int groupFeaturedProductId);
        void CreateGroupFeaturedProduct(GroupFeaturedProduct groupFeaturedProduct);
        void UpdateGroupFeaturedProduct(GroupFeaturedProduct groupFeaturedProduct);
        void DeleteGroupFeaturedProduct(GroupFeaturedProduct groupFeaturedProduct);
        IQueryable<GroupFeaturedProduct> GroupFeaturedProductsQuery(bool tracking);
        void DeleteGroupFeaturedProducts(ICollection<GroupFeaturedProduct> featuredProducts);

    }
}
