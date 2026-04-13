using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IUserPreferenceService
    {

        Task<IEnumerable<UserPreference>> GetAllUserPreferencesAsync();
        Task<UserPreference?> FindUserPreferenceByIdAsync(Guid userPreferenceId, bool tracking);
        Task CreateUserPreferenceAsync(NewUserPreferenceDto userPreference);
        Task UpdateUserPreferenceAsync(UserPreference userPreference);
        Task DeleteUserPreferenceAsync(Guid userPreferenceId);
        Task AddSellerToUserPreferenceAsync(Guid sellerProfileId,Guid userProfileId);
        Task<UserPreference?> GetSellersFollowedByUserAsync(Guid userProfileId);
        Task<IEnumerable<UserFollowerDto>> GetUserFollowingAsync(Guid userProfileId);
        Task<IEnumerable<UserFollowerDto>> GetSellerFollowersAsync(Guid sellerProfileId);
        Task<IEnumerable<UserPreferenceDto>> GetUserPreferencesAsync(Guid userProfileId);
        Task<bool> IsFollowingAsync(Guid UserProfileId, Guid sellerProfileId);


    }
}
