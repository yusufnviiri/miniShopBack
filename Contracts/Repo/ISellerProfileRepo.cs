using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ISellerProfileRepo
    {
        Task<IEnumerable<SellerProfileDto>> GetAllSellerProfiles();
        Task<SellerProfile?> FindSellerProfileById(Guid sellerProfileId, bool tracking);
        Task<MiniGroupDetailsDto?> FindMiniGroupDetailsById(Guid sellerProfileId);
        Task<SellerProfile?> FindSellerProfileBySellerId(Guid sellerId);
        Task<bool> CheckifUserIsSeller(Guid userProfileId);
        Task<bool> CheckifUserGroupIsSeller(Guid userGroupId);
        Task<Guid> GetSellerId(Guid sellerProfile);
        Task<Guid> GetSellerProfileId(Guid sellerId);
        void CreateSellerProfile(SellerProfile sellerProfile );
        void UpdateSellerProfile(SellerProfile sellerProfile);
        void DeleteSellerProfile(SellerProfile sellerProfile);
        Task<SellerShopDto?> GetSellerShopDetails(Guid sellerProfileId);
        Task<GroupProductsAndTradesList?> GetGroupProductsAndTradesList(Guid sellerProfileId);
        Task<IReadOnlyList<Guid>> GetGroupMemberSellerProfileIds(IReadOnlyList<Guid> memberIds);






    }
}
