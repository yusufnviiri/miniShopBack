using Entities.Models;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IUserProfileService
    {

        Task<(ICollection<UserProfileDto> usersData, MetaData MetaData)> GetAllUserProfilesAsync(UserRequestParameters requestParameters, CancellationToken ct = default);
        Task<(ICollection<UserProfileDto> sellersData, MetaData MetaData)> GetSellerUserProfilesAsync(UserRequestParameters requestParameters, CancellationToken ct = default);


        Task<IEnumerable<UserProfileDto>> GetAllUserProfilesWithoutGroupsAsync();
        Task<UserProfile?> FindUserProfileByIdAsync(Guid UserProfileId, bool tracking);
        Task<UserProfileDto?> ShowUserProfileAsync(Guid UserProfileId);
        Task<UserProfileDto?> ShowUserProfileBySlugAsync(string slug);

        Task<Guid> CreateUserProfileAsync(NewUserDataDto userProfile);
        Task<string> CreateUserProfileWithMemberAsync(GroupMemberJoinNewUserProfileDataDto dataDto);
        Task UpdateUserProfileAsync(NewUserDataDto userProfile, CancellationToken ct = default);
        Task DeleteUserProfileAsync(Guid userProfile, CancellationToken ct = default);
        Task VerifyPhoneAsync(VerifyOtpRequest request);
        Task CreateUserDevice(LoginRequestDto loginRequest, ApplicationUser user);
        Task<LoggedInUserDataDto?> GetLoggedInUserDataDtoAsync(Guid UserProfileId);
        Task<Guid> GetUserProfileIdFromIdentityUserAsync(string IdentityUserId);



    }
}
