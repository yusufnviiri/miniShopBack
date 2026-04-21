using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ISellerProfileService
    {
        Task<IEnumerable<SellerProfileDto>> GetAllSellerProfiles();
        Task<SellerProfile?> FindSellerProfileById(Guid sellerProfileId, bool tracking);
        Task CreateSellerProfile(SellerProfile sellerProfile);
        Task UpdateSellerProfile(SellerProfileDto sellerProfile);
        Task DeleteSellerProfile(Guid sellerProfileId);
        Task<SellerShopDto?> GetSellerShopDetailsAsync(Guid sellerProfileId);
        Task<GroupShopDto?> GetGroupShopDetails(Guid sellerProfileId,bool isMember);
        Task<GroupShopDto?> GetGroupShopDisplay(Guid sellerId);


    }
}
