using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class UserPreferenceRepo : RepositoryBase<UserPreference>, IUserPreferenceRepo
    {
        ApplicationDbContext _context;
        public UserPreferenceRepo(ApplicationDbContext _db) : base(_db)
        {
            _context = _db;
        }

        public async Task<IEnumerable<UserPreference>> GetAllUserPreferences() => await FindAll(false).ToListAsync();
        public IQueryable<UserPreference> UserPreferencesQueryData(Guid userProfileId, bool tracking) => FindByCondition(j => j.UserProfileId == userProfileId, tracking).Include(p => p.UserPreferenceSellerProfiles).Include(k => k.UserPreferenceCategories);
        public async Task<UserPreference?> FindUserPreferenceById(Guid userPreferenceId, bool tracking)
        {
            var userPreference = await FindByCondition(p => p.UserPreferenceId == userPreferenceId, tracking).FirstOrDefaultAsync();
            return userPreference;

        }
        public async Task<UserPreference?> FindUserPreferenceForUpdate(Guid userPreferenceId)
        {
            var userPreference = await FindByCondition(p => p.UserPreferenceId == userPreferenceId, true).FirstOrDefaultAsync();
            return userPreference;
        }
        public void CreateUserPreference(UserPreference userPreference) => CreateBase(userPreference);
        public void UpdateUserPreference(UserPreference userPreference) => UpdateBase(userPreference);
        public void DeleteUserPreference(UserPreference userPreference) => DeleteBase(userPreference);
        public async Task<UserPreference?> GetSellersFollwedByUser(Guid userProfileId)
        {
            var userPreference = await UserPreferencesQueryData(userProfileId, false).Select(p => new UserPreference()
            {
                UserPreferenceId = p.UserPreferenceId,
                UserProfileId = p.UserProfileId,
                UserPreferenceSellerProfiles = p.UserPreferenceSellerProfiles,
                UserPreferenceCategories = p.UserPreferenceCategories

            }).FirstOrDefaultAsync();
            return userPreference;

        }
        public async Task DeleteExistingUserPreference(Guid userProfileId)
        {
            var userPreferences = await UserPreferencesQueryData(userProfileId, true)
                                        .ToListAsync();

            if (!userPreferences.Any()) return;

            _context.UserPreferences.RemoveRange(userPreferences);
            await _context.SaveChangesAsync();
        }

        public async Task<UserPreference?> FindUserPreferenceWithCategoriesById(Guid userProfileId, bool tracking) => await FindByCondition(p => p.UserProfileId == userProfileId, tracking).Include(k => k.UserPreferenceCategories).FirstOrDefaultAsync();
        public async Task<UserPreference?> FindUserPreferenceWithSellersById(Guid userProfileId, bool tracking) => await FindByCondition(p => p.UserProfileId == userProfileId, tracking).Include(k => k.UserPreferenceSellerProfiles).FirstOrDefaultAsync();

        public async Task<IEnumerable<UserFollowerDto>> GetSellerFollowers(Guid sellerProfileId)
        {
            var followers = await _context.UserPreferenceSellerProfiles
                .Where(x => x.SellerProfileId == sellerProfileId)
                .Select(x => new UserFollowerDto
                {
                    FollowerId = x.UserPreference!=null?x.UserPreference.UserProfileId : Guid.Empty,
                    FollowerName = x.UserPreference != null ? x.UserPreference.UserProfile.IdentityUser.FirstName + " " + x.UserPreference.UserProfile.IdentityUser.LastName : string.Empty
                })
                .ToListAsync();

            return followers;
        }
        public async Task<IEnumerable<UserFollowerDto>> GetUserFollowing(Guid userProfileId)
        {
            var followers = await _context.UserPreferenceSellerProfiles
                .Where(x => x.UserPreference != null && x.UserPreference.UserProfileId == userProfileId)
                .Select(x => new UserFollowerDto
                {
                    FollowerId = x.SellerProfileId,
                    FollowerName = x.SellerProfile != null ? x.SellerProfile.SellerName : string.Empty
                })
                .ToListAsync();
            return followers;
        }
        public async Task<IEnumerable<UserPreferenceDto>> GetUserPreferences(Guid userProfileId)
        {
            var followers = await _context.UserPreferenceCategories
                  .Where(x => x.UserPreference!=null&&x.UserPreference.UserProfileId == userProfileId)
                  .Select(x => new UserPreferenceDto
                  {
                      CategoryId = x.CategoryId,

                      UserProfileId = x.UserPreference!=null ? x.UserPreference.UserProfileId : Guid.Empty,
                      PreferredCategory = x.Category!=null?x.Category.CategoryName : string.Empty,
                  }
             ) .ToListAsync();
            return followers;


        }
        public async Task<bool> IsFollowing(Guid UserProfileId, Guid sellerProfileId)
        {
            var isFollowing = await _context.UserPreferenceSellerProfiles
                .AnyAsync(x => x.UserPreference != null && x.UserPreference.UserProfileId == UserProfileId && x.SellerProfileId == sellerProfileId);
            return isFollowing;
        }

       


    }
}