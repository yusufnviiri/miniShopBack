using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IUserProfileService
    {
        Task<IEnumerable<UserProfileDto>> GetAllUserProfilesAsync();
        Task<IEnumerable<UserProfileDto>> GetAllUserProfilesWithoutGroupsAsync();
        Task<UserProfile?> FindUserProfileByIdAsync(Guid UserProfileId, bool tracking);
        Task<UserProfileDto?> ShowUserProfileAsync(Guid UserProfileId);
        Task<UserProfileDto?> ShowUserProfileBySlugAsync(string slug);

        Task<Guid> CreateUserProfileAsync(NewUserDataDto userProfile);
        Task CreateUserProfileWithMemberAsync(GroupMemberJoinNewUserProfileDataDto dataDto);
        Task UpdateUserProfileAsync(NewUserDataDto userProfile);
        Task DeleteUserProfileAsync(Guid userProfile);
        Task VerifyPhoneAsync(VerifyOtpRequest request);
        Task CreateUserDevice(LoginRequestDto loginRequest, ApplicationUser user);
        Task<LoggedInUserDataDto?> GetLoggedInUserDataDtoAsync(Guid UserProfileId);
        Task<Guid> GetUserProfileIdFromIdentityUserAsync(string IdentityUserId);
        Task<IEnumerable<UserProfileDto>> GetSellerUserProfilesAsync();



    }
}
