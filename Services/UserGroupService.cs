using AutoMapper;
using Contracts;
using Contracts.Lucene;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;
using Services.BusinessRules;
using Shared.Dtos;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Services
{
    internal sealed class UserGroupService:IUserGroupService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SlugService _slugService;
        private readonly IProductIndexer _indexer;
        private readonly ITradeIndexer _tradeIndexer;


        public UserGroupService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager, SlugService slugService, IProductIndexer productIndexer, ITradeIndexer tradeIndexer)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
            _tradeIndexer = tradeIndexer;
            _slugService = slugService;
            _indexer = productIndexer;


        }

        private string CreateUserGroupSlug(string name)
        {
            return _slugService.Generate(name);
        }


        public async  Task<IEnumerable<ShowUserGroupDto>> GetUserGroupsAsync()=>await _repoManager.UserGroupRepo.GetUserGroups();
     public async Task<ShowuserGroupWithMembersDto?> GetUserGroupWithMembersAsync(Guid userGroupId)=>await _repoManager.UserGroupRepo.GetUserGroupWithMembers(userGroupId);

        public async Task<ShowuserGroupWithMembersDto?> GetUserGroupWithMembersWithSlugAsync(string slug){
            var usergroupId = await _repoManager.UserGroupRepo.GetUserGroupIdBySlugName(slug);
            if (usergroupId == Guid.Empty)
            {
                throw new ObjectBadRequestExeption("User group data is null");
            }
            else
            {


                var group = await _repoManager.UserGroupRepo.GetUserGroupWithMembers(usergroupId);
                return group;
            }
        }

        public async Task<UserGroup?> FindUserGroupByIdAsync(Guid userGroupId, bool tracking) => await _repoManager.UserGroupRepo.FindUserGroupById(userGroupId,tracking);
    public async Task CreateUserGroupAsync(NewUserGroupDto userGroup, CancellationToken ct = default)
        {
            if (userGroup == null)
            {
                throw new ObjectBadRequestExeption("User group data is null");
            }
            Address address = new() {
             City = userGroup.City,
             Country = userGroup.Country,
             Company = userGroup.Company,
             Region = userGroup.Region,
            };
            var addressEntity = _repoManager.AddressRepo.CreateAddress(address);
            
    
        var userGroupEntity = _mapper.Map<UserGroup>(userGroup);
            await _repoManager.SaveRepoDataAsync();
            var numberOfUserGroups = await _repoManager.UserGroupRepo.NumberOfUserGroups();

            userGroupEntity.Slug = $"groups={CreateUserGroupSlug(userGroup.UserGroupName)}-{numberOfUserGroups + 1}";

            userGroupEntity.AddressId=addressEntity.AddressId;
            _repoManager.UserGroupRepo.CreateUserGroup(userGroupEntity);


            await _repoManager.SaveRepoDataAsync();

            var isSeller = await _repoManager.SellerProfileRepo.CheckifUserGroupIsSeller(userGroupEntity.UserGroupId);
            if (isSeller)
            {
                return;
            }
            else
            {
                SellerProfile sellerProfile = new()
                {
                    SellerName = userGroupEntity.UserGroupName,
                    SellerId = userGroupEntity.UserGroupId,
                    SellerTypeId = 2,
                    SellerTierId = 2,

                };
                sellerProfile.SellerPolicyId = userGroupEntity.GroupCategoryId switch
                {
                    1 => 1,
                    2 => 2,
                    3 => 3,
                    _ => 2,
                };
                var numberOfSellers = await _repoManager.SellerProfileRepo.NumberOfSellers();
                sellerProfile.Slug = $"seller={CreateUserGroupSlug(sellerProfile.SellerName)}-{numberOfSellers + 1}";
                _repoManager.SellerProfileRepo.CreateSellerProfile(sellerProfile);
                await _repoManager.SaveRepoDataAsync();
                await _indexer.QueueReindexBySellerAsync(sellerProfile.SellerProfileId,ct);
                await _tradeIndexer.QueueReindexBySellerAsync(sellerProfile.SellerProfileId, ct);

            }
        }
    public async Task UpdateUserGroupAsync(NewUserGroupDto userGroup)
        {
            if (userGroup == null)
            {
                throw new ObjectBadRequestExeption("User group data is null");
            }
            var existingUserGroup = await _repoManager.UserGroupRepo.FindUserGroupById(userGroup.UserGroupId, true);
           _mapper.Map(userGroup, existingUserGroup);
            _repoManager.UserGroupRepo.UpdateUserGroup(existingUserGroup);
            var address =await _repoManager.AddressRepo.FindAddressForUpdate(userGroup.AddressId);
            if (address is not null) {
                address.City = userGroup.City;
                address.Country = userGroup.Country;
                address.Company = userGroup.Company;
                address.Region = userGroup.Region;
            }

            await _repoManager.SaveRepoDataAsync();
        }
     public async  Task<ShowUserGroupDto> GetUserGroupByIdAsync(Guid userGroupId)=>await _repoManager.UserGroupRepo.GetUserGroupById(userGroupId);

        public async Task DeleteUserGroupAsync(Guid userGroupId)
        {
            var existingUserGroup = await _repoManager.UserGroupRepo.FindUserGroupById(userGroupId, true);
            if (existingUserGroup == null)
            {
                throw new ItemNotFoundException(userGroupId);
            }
            _repoManager.UserGroupRepo.DeleteUserGroup(existingUserGroup);
            await _repoManager.SaveRepoDataAsync();
        }
    }
}
