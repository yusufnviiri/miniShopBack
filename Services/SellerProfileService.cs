using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class SellerProfileService : ISellerProfileService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public SellerProfileService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }
        public async  Task<IEnumerable<SellerProfileDto>> GetAllSellerProfiles()=>await _repoManager.SellerProfileRepo.GetAllSellerProfiles();
        public async Task<SellerProfile?> FindSellerProfileById(Guid sellerProfileId, bool tracking)=>await _repoManager.SellerProfileRepo.FindSellerProfileById(sellerProfileId,tracking);
        public async Task CreateSellerProfile(SellerProfile sellerProfile)
        {
            var IsExist = await _repoManager.SellerProfileRepo.CheckifUserIsSeller(sellerProfile.SellerId);
            if (!IsExist)
            {
                _repoManager.SellerProfileRepo.CreateSellerProfile(sellerProfile);
                await _repoManager.SaveRepoDataAsync();
                var user = await _repoManager.UserProfileRepo.FindUserProfileById(sellerProfile.SellerId,true);
                if (user != null)
                {
                    user.SellerProfileId = sellerProfile.SellerProfileId;
                    _repoManager.UserProfileRepo.UpdateUserProfile(user);
                    var groupMember = await _repoManager.GroupMemberRepo.FindGroupMemberByUserProfileId(sellerProfile.SellerId);
                    if (groupMember != null&& groupMember.UserGroupId!=Guid.Empty)
                    {

 

                       GroupSeller groupSeller = new ()
                        {
                            SellerProfileId = sellerProfile.SellerProfileId,
                            UserGroupId= groupMember.UserGroupId,
                            GroupMemberId= groupMember.GroupMemberId
                       };
                        _repoManager.GroupSellerRepo.CreateGroupSeller(groupSeller);

                    }
                    await _repoManager.SaveRepoDataAsync();
                }

                }
            else
            {
                
                throw new ObjectBadRequestExeption($"Seller Profile for user with id: {sellerProfile.SellerId} already exists");
            }
        }
        public async Task UpdateSellerProfile(SellerProfileDto sellerProfile)
        {
            var existingSellerProfile = await _repoManager.SellerProfileRepo.FindSellerProfileById(sellerProfile.SellerProfileId, true);
            if (existingSellerProfile == null)
            {
                _logger.LogError($"Seller Profile with id: {sellerProfile.SellerProfileId} not found");
                throw new ObjectBadRequestExeption($"object with id {sellerProfile.SellerProfileId} not found");
            }

            existingSellerProfile.SellerTierId = sellerProfile.SellerTierId;
            existingSellerProfile. SellerTypeId = sellerProfile.SellerTypeId;
            existingSellerProfile.SellerPolicyId = sellerProfile.SellerPolicyId;

        _repoManager.SellerProfileRepo.UpdateSellerProfile(existingSellerProfile);
            await _repoManager.SaveRepoDataAsync();

        }
        public async Task DeleteSellerProfile(Guid sellerProfileId)
        {
            var existingBuyerProfile = await _repoManager.SellerProfileRepo.FindSellerProfileById(sellerProfileId, tracking: true);
            if (existingBuyerProfile is null)
            {
                _logger.LogError($"Seller Profile with id: {sellerProfileId} not found.");
                throw new ObjectBadRequestExeption($"object with id {sellerProfileId} not found");
            }
        }

        public async Task<SellerShopDto?> GetSellerShopDetailsAsync(Guid sellerProfileId)
        {
            var groupData = await _repoManager.UserProfileRepo.GetSellerGroupsIds(sellerProfileId);
            var sellerShopDto = await _repoManager.SellerProfileRepo.GetSellerShopDetails(sellerProfileId);
            var followers = await _repoManager.UserPreferenceRepo.GetSellerFollowers(sellerProfileId);
            if (groupData != null && sellerShopDto!=null)
            {
                foreach (var item in groupData)
                {
                    SellerGroupDto groupDto = new SellerGroupDto()
                    {
                        GroupName = await _repoManager.UserGroupRepo.GetGroupGroupName(item) ?? "",
                        GroupId = item
                    };
                    sellerShopDto.Groups.Add(groupDto);
                }



            }
            if (followers != null && sellerShopDto != null)
            {
                sellerShopDto.Followers = followers;
            }
            return sellerShopDto;
        }
        public async Task<GroupShopDto?> GetGroupShopDetails(Guid sellerId, bool isMember)

        {
            GroupShopDto groupShopDto = new();

            var sellerProfileId = await _repoManager.SellerProfileRepo.GetSellerProfileId(sellerId);
            //var groupDetails = await _repoManager.SellerProfileRepo.FindMiniGroupDetailsById(sellerProfileId);
            //if (groupDetails != null)
            //{
            //    groupShopDto.GroupDetails = groupDetails;
            //}
            //if (isMember)
            //{
            //    var groupShopDetails = await _repoManager.SellerProfileRepo.GetGroupProductsAndTradesList(sellerProfileId);
            //    groupShopDto.GroupProducts = groupShopDetails;
            //}
            var memberprofileIds = await _repoManager.GroupMemberRepo.GetGroupMemberProfileIds(sellerId);
            var membersellerProfileIds = await _repoManager.SellerProfileRepo.GetGroupMemberSellerProfileIds(memberprofileIds);
            if (membersellerProfileIds.Any())
            {
                var memberProducts = await _repoManager.ProductRepo.GetGroupMembersProducts([.. membersellerProfileIds]);
                var memberTrades = await _repoManager.TradeRepo.GetGroupMembersTrades([.. membersellerProfileIds]);
                groupShopDto.MemberProducts = memberProducts;
                groupShopDto.MemberTrades = memberTrades;

            }
            return groupShopDto;

        }


    }
}