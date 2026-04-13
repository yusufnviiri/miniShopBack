using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IUserPreferenceRepo
    {
        Task<IEnumerable<UserPreference>> GetAllUserPreferences();
        IQueryable<UserPreference> UserPreferencesQueryData(Guid userProfileId, bool tracking);
        Task<UserPreference?> FindUserPreferenceById(Guid userPreferenceId, bool tracking);
        Task<UserPreference?> FindUserPreferenceWithCategoriesById(Guid userProfileId, bool tracking);
        Task<UserPreference?> FindUserPreferenceWithSellersById(Guid userProfileId, bool tracking);

        Task<UserPreference?> FindUserPreferenceForUpdate(Guid userPreferenceId);
        void CreateUserPreference(UserPreference userPreference);
        void UpdateUserPreference(UserPreference userPreference);
        void DeleteUserPreference(UserPreference userPreference);
        Task<UserPreference?> GetSellersFollwedByUser(Guid userProfileId);
        Task DeleteExistingUserPreference(Guid userProfileId);
        Task<IEnumerable<UserFollowerDto>> GetUserFollowing(Guid userProfileId);
        Task<IEnumerable<UserFollowerDto>> GetSellerFollowers(Guid sellerProfileId);
        Task<IEnumerable<UserPreferenceDto>> GetUserPreferences(Guid userProfileId);
        Task<bool> IsFollowing(Guid UserProfileId, Guid sellerProfileId);




    }

}
