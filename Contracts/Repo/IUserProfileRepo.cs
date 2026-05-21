using Entities.Models;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IUserProfileRepo
    {

        Task<PagedList<UserProfileDto>> GetAllUserProfiles(UserRequestParameters request, CancellationToken token = default);
        Task<PagedList<UserProfileDto>> GetSellerUserProfiles(UserRequestParameters request,CancellationToken token=default);



        IQueryable<UserProfile> FindUserProfilesAsQueryable(Guid UserProfileId);
        Task<IEnumerable<UserProfileDto>> GetAllUserProfilesWithoutGroups();
        Task<UserProfileDto?> ShowUserProfile(Guid UserProfileId);
        Task<Guid> GetUserProfileIdFromIdentityUser(string IdentityUserId);
        Task<UserProfile?> FindUserProfileById(Guid UserProfileId, bool tracking);
        UserProfile CreateUserProfile(UserProfile userProfile );
        void UpdateUserProfile(UserProfile userProfile);
        void DeleteUserProfile(UserProfile userProfile);
        Task <IReadOnlyCollection<Guid>> GetSellerGroupsIds(Guid sellerProfileId);
        Task<ICollection<GroupMemberRolesDto>> GetGroupMemberRolesDtos(Guid UserProfileId);
        Task<LoggedInUserDataDto?> GetLoggedInUserDataDto(Guid UserProfileId);
        Task<string?> GetUserContact(Guid userProfileId);
        Task<int> NumberOfUserProfiles();
        Task<long> NextUserSlugNumberAsync();

        Task<Guid> GetUserProfileIdBySlugName(string slug);









    }
}
