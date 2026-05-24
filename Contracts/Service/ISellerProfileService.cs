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
        Task CreateSellerProfile(SellerProfile sellerProfile, CancellationToken ct = default);
        Task MakeGroupMemberSellerByGroupAdmin(GroupMemberSellerprofileDto sellerProfile, CancellationToken ct = default);

        //Task CreateSellerProfileByAppAdmin(SellerProfile sellerProfile, CancellationToken ct = default);
        //Task CreateSellerProfileByGroupAdmin(SellerProfile sellerProfile, CancellationToken ct = default);
        //Task MakeGroupMemberSellerByAppAdmin(GroupMemberSellerprofileDto sellerProfile, CancellationToken ct = default);




        Task UpdateSellerProfile(SellerProfileDto sellerProfile, CancellationToken ct = default);
        Task DeleteSellerProfile(Guid sellerProfileId, CancellationToken ct = default);
        Task<SellerShopDto?> GetSellerShopDetailsAsync(Guid sellerProfileId);
        Task<SellerShopDto?> GetSellerShopDetailsBySlugAsync(string slug);

        Task<GroupShopDto?> GetGroupShopDetails(Guid sellerProfileId,bool isMember);
        Task<GroupShopDto?> GetGroupShopDisplayBySlugAsync(string slug);

        Task<GroupShopDto?> GetGroupShopDetailsBySlugAsync(string slug, bool isMember);
        Task<GroupShopDto?> GetGroupShopDisplay(Guid sellerId);
        Task<List<SlugInfo>> GetAllSellerSlugsAsync();


    }
}
