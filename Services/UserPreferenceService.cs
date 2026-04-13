using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class UserPreferenceService : IUserPreferenceService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;

        public UserPreferenceService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

        public async  Task<IEnumerable<UserPreference>> GetAllUserPreferencesAsync()=>await _repoManager.UserPreferenceRepo.GetAllUserPreferences();
        public async Task<UserPreference?> FindUserPreferenceByIdAsync(Guid userPreferenceId, bool tracking)=> await _repoManager.UserPreferenceRepo.FindUserPreferenceById(userPreferenceId,tracking);
        public async Task CreateUserPreferenceAsync(NewUserPreferenceDto userPreference)
        {
            if (userPreference == null) return;

            UserPreference newUserPreference = new()
            {
                UserPreferenceId = Guid.NewGuid(),
                UserProfileId = userPreference.UserProfileId,
                UserPreferenceCategories = new List<UserPreferenceCategory>()
            };
            await _repoManager.UserPreferenceRepo.DeleteExistingUserPreference(userPreference.UserProfileId);
            if (userPreference.CategoryIds != null && userPreference.CategoryIds.Any())
            {
                foreach (var item in userPreference.CategoryIds)
                {
                    var category = await _repoManager.CategoryRepo.FindCategoryById(item, false);
                    if (category != null)
                        newUserPreference.UserPreferenceCategories.Add(new UserPreferenceCategory
                        {
                            CategoryId = item
                        });
                }
            }

            _repoManager.UserPreferenceRepo.CreateUserPreference(newUserPreference);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateUserPreferenceAsync(UserPreference userPreference)
        {
            if (userPreference == null && userPreference.UserPreferenceId == Guid.Empty)
            {
                throw new ObjectBadRequestExeption($"Object properties not set");

            }
            else
            {
                var userPreferenceForUpDate = await _repoManager.UserPreferenceRepo.FindUserPreferenceForUpdate(userPreference.UserPreferenceId);
                if (userPreferenceForUpDate != null)
                {
                    

                }
            }
        }
        public async Task DeleteUserPreferenceAsync(Guid userPreferenceId)
        {
            var userPreference = await _repoManager.UserPreferenceRepo.FindUserPreferenceForUpdate(userPreferenceId);
            if (userPreference != null)
            {
                _repoManager.UserPreferenceRepo.DeleteUserPreference(userPreference);
            }
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task AddSellerToUserPreferenceAsync(Guid sellerProfileId, Guid userProfileId)
        {
            var userProfile = await _repoManager.UserProfileRepo.FindUserProfileById(userProfileId, false);
            if (userProfile == null)
            {
                throw new ObjectBadRequestExeption($"user with profile {userProfileId} does not exixt");
            }
            var sellerProfile = await _repoManager.SellerProfileRepo.FindSellerProfileById(sellerProfileId, false);
            if (sellerProfile == null)
            {
                throw new ObjectBadRequestExeption($"user with profile {sellerProfile} does not exixt");
            }
            var userPreference = await _repoManager.UserPreferenceRepo.FindUserPreferenceWithSellersById(userProfileId, true);
            if (userPreference != null)
            {
                userPreference.UserPreferenceSellerProfiles.Add(new UserPreferenceSellerProfile
                {
                    SellerProfileId = sellerProfile.SellerProfileId
                }); await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                UserPreference newUserPreference = new()
                {
                    UserPreferenceId = Guid.NewGuid(),
                    UserProfileId = userProfileId,
                    UserPreferenceSellerProfiles = [ new UserPreferenceSellerProfile
                {
                    SellerProfileId = sellerProfile.SellerProfileId
                } ]
                };

                _repoManager.UserPreferenceRepo.CreateUserPreference(newUserPreference);

                await _repoManager.SaveRepoDataAsync();



            }
        }

        public async Task<UserPreference?> GetSellersFollowedByUserAsync(Guid userProfileId)
        {
            var userPreference = await _repoManager.UserPreferenceRepo.GetSellersFollwedByUser(userProfileId);
            return userPreference;
        }
       public async Task<IEnumerable<UserFollowerDto>> GetUserFollowingAsync(Guid userProfileId)=>await _repoManager.UserPreferenceRepo.GetUserFollowing(userProfileId);
        public async Task<IEnumerable<UserFollowerDto>> GetSellerFollowersAsync(Guid sellerProfileId)=>await _repoManager.UserPreferenceRepo.GetSellerFollowers(sellerProfileId);
        public async Task<IEnumerable<UserPreferenceDto>> GetUserPreferencesAsync(Guid userProfileId)=>await _repoManager.UserPreferenceRepo.GetUserPreferences(userProfileId);
        public async Task<bool> IsFollowingAsync(Guid UserProfileId, Guid sellerProfileId) => await _repoManager.UserPreferenceRepo.IsFollowing(UserProfileId, sellerProfileId);

    }
}
