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
using System.Threading.Tasks;

namespace Services
{
internal sealed class UserGroupSellerService : IUserGroupSellerService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserGroupSellerService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

      public async  Task<IEnumerable<UserGroupSellerDto>> GetAllUserGroupSellersAsync()=> await _repoManager.UserGroupSellerRepo.GetAllUserGroupSellers();
        public async Task<UserGroupSeller?> FindUserGroupSellerByIdAsync(Guid sellerGroupId, bool tracking)=> await _repoManager.UserGroupSellerRepo.FindUserGroupSellerById(sellerGroupId, tracking);
        public async Task CreateUserGroupSellerAsync(UserGroupSellerDto userGroupSeller)
        {
            _repoManager.UserGroupSellerRepo.CreateUserGroupSeller(_mapper.Map<UserGroupSeller>(userGroupSeller));
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateUserGroupSellerAsync(UserGroupSellerDto userGroupSeller)
        {
            var existingUserGroupSeller = await _repoManager.UserGroupSellerRepo.FindUserGroupSellerById(userGroupSeller.UserGroupSellerId, true);
            if (existingUserGroupSeller == null)
            {
                _logger.LogError($"UserGroupSeller with id: {userGroupSeller.UserGroupSellerId} not found.");
                throw new ObjectBadRequestExeption("UserGroupSeller not found.");
            }
            existingUserGroupSeller.SellerProfileId = userGroupSeller.SellerProfileId;
            existingUserGroupSeller.UserGroupId = userGroupSeller.UserGroupId;
            _repoManager.UserGroupSellerRepo.UpdateUserGroupSeller(existingUserGroupSeller);
            await _repoManager.SaveRepoDataAsync();

        }
        public async Task DeleteUserGroupSellerAsync(Guid userGroupSellerId)
        {
            var existingUserGroupSeller = await _repoManager.UserGroupSellerRepo.FindUserGroupSellerById(userGroupSellerId, true);
            if (existingUserGroupSeller == null)
            {
                _logger.LogError($"UserGroupSeller with id: {userGroupSellerId} not found.");
                throw new ObjectBadRequestExeption("UserGroupSeller not found.");
            }
            _repoManager.UserGroupSellerRepo.DeleteUserGroupSeller(existingUserGroupSeller);
            await _repoManager.SaveRepoDataAsync();

        }
    }
}
