using AutoMapper;
using Contracts;
using Contracts.Lucene;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Services.BusinessRules;
using Services.Lucene;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    internal sealed class SellerProfileService : ISellerProfileService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SlugService _slugService;
        private readonly IProductIndexer _indexer;
        private readonly ITradeIndexer _tradeIndexer;
        private readonly IUserIndexer _userIndexer;




        public SellerProfileService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager, SlugService slugService, IProductIndexer productIndexer, ITradeIndexer tradeIndexer, IUserIndexer userIndexer)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
            _slugService = slugService;
            _indexer = productIndexer;
            _tradeIndexer = tradeIndexer;
            _userIndexer = userIndexer;


        }

        private string CreateSellerProfileSlug(string name)
        {
            return _slugService.Generate(name);
        }
        public async  Task<IEnumerable<SellerProfileDto>> GetAllSellerProfiles()=>await _repoManager.SellerProfileRepo.GetAllSellerProfiles();
        public async Task<SellerProfile?> FindSellerProfileById(Guid sellerProfileId, bool tracking)=>await _repoManager.SellerProfileRepo.FindSellerProfileById(sellerProfileId,tracking);
        public async Task CreateSellerProfile(SellerProfile sellerProfile, CancellationToken ct = default)
        {
            var IsExist = await _repoManager.SellerProfileRepo.CheckifUserIsSeller(sellerProfile.SellerId);
            if (!IsExist)
            {

                var numberOfSellers = await _repoManager.SellerProfileRepo.NumberOfSellers();
                sellerProfile.Slug = $"merchant-{CreateSellerProfileSlug(sellerProfile.SellerName)}-{numberOfSellers + 1}";
                
                
                _repoManager.SellerProfileRepo.CreateSellerProfile(sellerProfile);
                await _repoManager.SaveRepoDataAsync();
                var user = await _repoManager.UserProfileRepo.FindUserProfileById(sellerProfile.SellerId,true);
                if (user != null)
                {
                    user.SellerProfileId = sellerProfile.SellerProfileId;
                    _repoManager.UserProfileRepo.UpdateUserProfile(user);
                    await _userIndexer.QueueIndexAsync(sellerProfile.SellerId, ct);
                                     
                      

                    
                    await _repoManager.SaveRepoDataAsync();
                    await _indexer.QueueReindexBySellerAsync(sellerProfile.SellerProfileId,ct);
                    await _tradeIndexer.QueueReindexBySellerAsync(sellerProfile.SellerProfileId, ct);

                }

            }
            else
            {
                
                throw new ObjectBadRequestExeption($"Seller Profile for user with id: {sellerProfile.SellerId} already exists");
            }
        }

        public async Task MakeGroupMemberSeller(GroupMemberSellerprofileDto sellerProfile, CancellationToken ct = default)
        {
            UserProfile?  user ;

            var IsExist = await _repoManager.SellerProfileRepo.CheckifUserIsSeller(sellerProfile.SellerId);
            if (!IsExist)
            {

                var numberOfSellers = await _repoManager.SellerProfileRepo.NumberOfSellers();
                sellerProfile.Slug = $"merchant-{CreateSellerProfileSlug(sellerProfile.SellerName)}-{numberOfSellers + 1}";
                var newSellerProfile = _mapper.Map<SellerProfile>(sellerProfile);

                _repoManager.SellerProfileRepo.CreateSellerProfile(newSellerProfile);
                await _repoManager.SaveRepoDataAsync();
                user = await _repoManager.UserProfileRepo.FindUserProfileById(sellerProfile.SellerId, true);
                if (user != null)
                {
                    user.SellerProfileId = sellerProfile.SellerProfileId;
                    _repoManager.UserProfileRepo.UpdateUserProfile(user);
                    await _userIndexer.QueueIndexAsync(sellerProfile.SellerId, ct);


                    await _indexer.QueueReindexBySellerAsync(sellerProfile.SellerProfileId, ct);
                    await _tradeIndexer.QueueReindexBySellerAsync(sellerProfile.SellerProfileId, ct);

                }

            }
            else
            {
                var existingSellerprofile = await _repoManager.SellerProfileRepo.GetSellerProfileId(sellerProfile.SellerId);
                if (existingSellerprofile == Guid.Empty)
                {
                    throw new ObjectBadRequestExeption($"Insuffiecient Seller Profile Data ");

                }
                else {
                    sellerProfile.SellerProfileId = existingSellerprofile;
                }
            }
                    var groupMember = await _repoManager.GroupMemberRepo.FindGroupMemberByUserProfileIdWithTracking(sellerProfile.SellerId,sellerProfile.UserGroupId,true);
            if (groupMember != null && groupMember.UserGroupId != Guid.Empty)
            {
                var groupSellerExists = await _repoManager.GroupSellerRepo.CheckIfSellerExistsInGroup(sellerProfile.SellerProfileId, sellerProfile.UserGroupId);
                if (!groupSellerExists)
                {


                    GroupSeller groupSeller = new()
                    {
                        SellerProfileId = sellerProfile.SellerProfileId,
                        UserGroupId = sellerProfile.UserGroupId,
                        GroupMemberId = groupMember.GroupMemberId,

                    };
                    _repoManager.GroupSellerRepo.CreateGroupSeller(groupSeller);
                    groupMember.GroupSellers.Add(groupSeller);
                }

            }
            await _repoManager.SaveRepoDataAsync();
            await _indexer.QueueReindexBySellerAsync(sellerProfile.SellerProfileId, ct);
            await _tradeIndexer.QueueReindexBySellerAsync(sellerProfile.SellerProfileId, ct);





        }

        public async Task UpdateSellerProfile(SellerProfileDto sellerProfile, CancellationToken ct = default)
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
            await _indexer.QueueReindexBySellerAsync(sellerProfile.SellerProfileId, ct);
            await _tradeIndexer.QueueReindexBySellerAsync(sellerProfile.SellerProfileId, ct);

        }
        public async Task DeleteSellerProfile(Guid sellerProfileId, CancellationToken ct = default)
        {
            var existingBuyerProfile = await _repoManager.SellerProfileRepo.FindSellerProfileById(sellerProfileId, tracking: true);
            if (existingBuyerProfile is null)
            {
                _logger.LogError($"Seller Profile with id: {sellerProfileId} not found.");
                throw new ObjectBadRequestExeption($"object with id {sellerProfileId} not found");
            }
            else
            {
                _repoManager.SellerProfileRepo.DeleteSellerProfile(existingBuyerProfile);
                await _repoManager.SaveRepoDataAsync();
                await _indexer.QueueReindexBySellerAsync(sellerProfileId, ct);
                await _tradeIndexer.QueueReindexBySellerAsync(sellerProfileId, ct);
            } }

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
                        GroupId = item,
                        SellerSlugName= await _repoManager.UserGroupRepo.GetGroupGroupSlugName(item) ?? ""
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
            var groupDetails = await _repoManager.SellerProfileRepo.FindMiniGroupDetailsById(sellerProfileId);
            if (groupDetails != null)
            {
                groupShopDto.GroupDetails = groupDetails;
                groupShopDto.SellerSlug = groupDetails.SlugName;
            }
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
                var memberTrades = await _repoManager.TradeRepo.GetGroupMembersTrades([.. membersellerProfileIds],sellerId);
                groupShopDto.MemberProducts = memberProducts;
                groupShopDto.MemberTrades = memberTrades;

            }
            return groupShopDto;

        }






        public async Task<GroupShopDto?> GetGroupShopDisplay(Guid sellerId)

        {
            GroupShopDto groupShopDto = new();

            var sellerProfileId = await _repoManager.SellerProfileRepo.GetSellerProfileId(sellerId);
            var groupDetails = await _repoManager.SellerProfileRepo.FindMiniGroupDetailsById(sellerProfileId);
            if (groupDetails != null)
            {
                groupShopDto.GroupDetails = groupDetails;
                groupShopDto.SellerSlug = groupDetails.SlugName;

            }
            //if (isMember)
            //{
            //    var groupShopDetails = await _repoManager.SellerProfileRepo.GetGroupProductsAndTradesList(sellerProfileId);
            //    groupShopDto.GroupProducts = groupShopDetails;
            //}
            var memberprofileIds = await _repoManager.GroupMemberRepo.GetGroupMemberProfileIds(sellerId);
            var membersellerProfileIds = await _repoManager.SellerProfileRepo.GetGroupMemberSellerProfileIds(memberprofileIds);
            if (membersellerProfileIds.Any())
            {
                var memberProducts = await _repoManager.ProductRepo.GetGroupMembersForDisplayProducts([.. membersellerProfileIds],sellerId);
                var memberTrades = await _repoManager.TradeRepo.GetGroupMembersTrades([.. membersellerProfileIds],sellerId);
                groupShopDto.MemberProducts = memberProducts;
                groupShopDto.MemberTrades = memberTrades;

            }
            return groupShopDto;

        }

        public async Task<GroupShopDto?> GetGroupShopDisplayBySlugAsync(string slug)

        {
            GroupShopDto groupShopDto = new();

            var sellerId = await _repoManager.UserGroupRepo.GetUserGroupIdBySlugName(slug);
            var sellerProfileId = await _repoManager.SellerProfileRepo.GetSellerProfileId(sellerId);
            var groupDetails = await _repoManager.SellerProfileRepo.FindMiniGroupDetailsById(sellerProfileId);
            if (groupDetails != null)
            {
                groupShopDto.GroupDetails = groupDetails;
                groupShopDto.SellerSlug = slug;
            }
            //if (isMember)
            //{
            //    var groupShopDetails = await _repoManager.SellerProfileRepo.GetGroupProductsAndTradesList(sellerProfileId);
            //    groupShopDto.GroupProducts = groupShopDetails;
            //}
            var memberprofileIds = await _repoManager.GroupMemberRepo.GetGroupMemberProfileIds(sellerId);
            var membersellerProfileIds = await _repoManager.SellerProfileRepo.GetGroupMemberSellerProfileIds(memberprofileIds);
            if (membersellerProfileIds.Any())
            {
                var memberProducts = await _repoManager.ProductRepo.GetGroupMembersForDisplayProducts([.. membersellerProfileIds], sellerId);
                var memberTrades = await _repoManager.TradeRepo.GetGroupMembersTrades([.. membersellerProfileIds], sellerId);
                groupShopDto.MemberProducts = memberProducts;
                groupShopDto.MemberTrades = memberTrades;
                groupShopDto.SellerSlug = slug;

            }
            return groupShopDto;

        }

        public async Task<SellerShopDto?> GetSellerShopDetailsBySlugAsync(string slug)
        {

   var sellerProfileId = await _repoManager.SellerProfileRepo.GetSellerProfileIdBySlugName(slug);
            if (sellerProfileId != Guid.Empty)
            {
                return await GetSellerShopDetailsAsync(sellerProfileId);
            }
            else
            {
                _logger.LogError($"Seller Profile with slug: {slug} not found.");
                throw new ObjectBadRequestExeption($"object with slug {slug} not found");
            }
        }


        public async Task<GroupShopDto?> GetGroupShopDetailsBySlugAsync(string slug, bool isMember)

        {
            GroupShopDto groupShopDto = new();
            var sellerId = await _repoManager.UserGroupRepo.GetUserGroupIdBySlugName(slug);


            var sellerProfileId = await _repoManager.SellerProfileRepo.GetSellerProfileId(sellerId);
            var groupDetails = await _repoManager.SellerProfileRepo.FindMiniGroupDetailsById(sellerProfileId);
            if (groupDetails != null)
            {
                groupShopDto.GroupDetails = groupDetails;
                groupShopDto.SellerSlug = slug;
            }
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
                var memberTrades = await _repoManager.TradeRepo.GetGroupMembersTrades([.. membersellerProfileIds], sellerId);
                groupShopDto.MemberProducts = memberProducts;
                groupShopDto.MemberTrades = memberTrades;

            }
            return groupShopDto;

        }

    }
}