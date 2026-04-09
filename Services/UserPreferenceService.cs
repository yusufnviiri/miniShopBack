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
            UserPreference newUserPreference = new();
            ICollection<UserPreference> userPreferences = [];
            if (userPreference != null)
            {
                if (userPreference.CategoryIds != null && userPreference.CategoryIds.Any())
                {
                    ICollection<Category> categories = [];
                    foreach (var item in userPreference.CategoryIds)
                    {
                        var category = await _repoManager.CategoryRepo.FindCategoryById(item, false);
                        if (category != null)
                        {
                            categories.Add(category);
                        }
                        newUserPreference.Categories = categories;
                        newUserPreference.UserPreferenceId = userPreference.UserProfileId;
                        _repoManager.UserPreferenceRepo.CreateUserPreference(newUserPreference);
                        await _repoManager.SaveRepoDataAsync();


                    }

                }

            }
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

    }
}
