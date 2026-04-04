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
    public class UserProfileRepo : RepositoryBase<UserProfile>, IUserProfileRepo
    {
        public UserProfileRepo(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<UserProfileDto>> GetAllUserProfilesWithoutGroups()
        {
            return await FindAll(false)
                .Select(p => new UserProfileDto
                {
                    UserProfileId = p.UserProfileId,
                    IsSeller = p.SellerProfileId != Guid.Empty && p.SellerProfileId != null ? true : false,
                    ApplicationUser = new ShowApplicationUserDto
                    {
                        IdentityUserId = p.IdentityUserId,
                        FirstName = p.IdentityUser.FirstName,
                        LastName = p.IdentityUser.LastName
                    },

                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<UserProfileDto>> GetSellerUserProfiles()
        {
            return await FindByCondition(p => p.SellerProfileId !=Guid.Empty && p.SellerProfile!=null&&p.ActiveGroupId==null, false)
                .Select(p => new UserProfileDto
                {
                    UserProfileId = p.UserProfileId,
                    IsSeller = p.SellerProfileId != Guid.Empty && p.SellerProfileId != null ? true : false,
                    ApplicationUser = new ShowApplicationUserDto
                    {
                        IdentityUserId = p.IdentityUserId,
                        FirstName = p.IdentityUser.FirstName,
                        LastName = p.IdentityUser.LastName,
                        PhoneNumber = p.IdentityUser.PhoneNumber,
                        City=p.Address!=null?p.Address.City:"Not Specified",
                        Country = p.Address != null ? p.Address.Country : "Not Specified",
                        Company =p.SellerProfile!=null?p.SellerProfile.SellerName : "Not Specified",
                        SellerId=p.SellerProfileId,
                    },

                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }



        public async Task<IEnumerable<UserProfileDto>> GetAllUserProfiles()
        {
            return await FindAll(false)
                .Select(p => new UserProfileDto
                {
                    UserProfileId = p.UserProfileId,
                    IsSeller = p.SellerProfileId != Guid.Empty && p.SellerProfileId != null ? true : false,


                    UserGroups = p.GroupMemberships.Any()
                        ? p.GroupMemberships.Select(g => new ShowUserGroupDto
                        {
                            UserGroupId = g.Group.UserGroupId,
                            UserGroupName = g.Group.UserGroupName,
                            City = g.Group.Address.City,
                            Region = g.Group.Address.Region,
                            Country = g.Group.Address.Country,
                            Company = g.Group.Address.Company
                        }).ToList()
                        : new List<ShowUserGroupDto>(),

                    ApplicationUser = new ShowApplicationUserDto
                    {
                        IdentityUserId = p.IdentityUserId,
                        FirstName = p.IdentityUser.FirstName,
                        LastName = p.IdentityUser.LastName
                    },

                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }
        public async Task<UserProfileDto?> ShowUserProfile(Guid UserProfileId)
        {

            return await FindByCondition(p => p.UserProfileId == UserProfileId, false).Select(p => new UserProfileDto
            {
                UserProfileId = p.UserProfileId,
                IsSeller = p.SellerProfileId != Guid.Empty && p.SellerProfileId != null ? true : false,

                UserGroups = p.GroupMemberships.Any()
                          ? p.GroupMemberships.Select(g => new ShowUserGroupDto
                          {
                              UserGroupId = g.Group.UserGroupId,
                              UserGroupName = g.Group.UserGroupName,

                          }).ToList()
                          : new List<ShowUserGroupDto>(),

                ApplicationUser = new ShowApplicationUserDto
                {
                    IdentityUserId = p.IdentityUserId,
                    FirstName = p.IdentityUser.FirstName,
                    LastName = p.IdentityUser.LastName,
                    PhoneNumber = p.IdentityUser.PhoneNumber,
                    Email = p.IdentityUser.Email
                },
                AddressId = p.AddressId,
                CreatedAt = p.CreatedAt,
                Address = new Address()
                {
                    City = p.Address.City,
                    Region = p.Address.Region,
                    Country = p.Address.Country,
                    Company = p.Address.Company

                }
            })
                  .FirstOrDefaultAsync();

        }

        public Task<UserProfile?> FindUserProfileById(Guid UserProfileId, bool tracking)
        {
            return FindByCondition(p => p.UserProfileId == UserProfileId, tracking).FirstOrDefaultAsync();
        }
        public IQueryable<UserProfile> FindUserProfilesAsQueryable(Guid UserProfileId)
        {
            return FindByCondition(p => p.UserProfileId == UserProfileId, false);
        }
        public UserProfile CreateUserProfile(UserProfile userProfile) { CreateBase(userProfile); return userProfile; }
        public void UpdateUserProfile(UserProfile userProfile) => UpdateBase(userProfile);
        public void DeleteUserProfile(UserProfile userProfile) => DeleteBase(userProfile);
        public Task<Guid> GetUserProfileIdFromIdentityUser(string IdentityUserId) => FindByCondition(p => p.IdentityUserId == IdentityUserId, false).Select(k => k.UserProfileId).FirstAsync();
        public async Task<IReadOnlyCollection<Guid>> GetSellerGroupsIds(Guid sellerProfileId)
        {
            return await FindByCondition(p => p.SellerProfileId == sellerProfileId, trackChanges: false)
                .SelectMany(p => p.GroupMemberships)
                .Select(gm => gm.UserGroupId)
                .Distinct()
                .ToListAsync();
        }
        public async Task<ICollection<GroupMemberRolesDto>> GetGroupMemberRolesDtos(Guid UserProfileId)
        {
            return await FindByCondition(p => p.UserProfileId == UserProfileId, trackChanges: false)
                .SelectMany(p => p.GroupMemberships)
                .Select(gm => new GroupMemberRolesDto
                {
                    GroupId = gm.UserGroupId,
                    Role = gm.GroupRole != null ? gm.GroupRole.Description : "Member",
                    GroupName = gm.Group.UserGroupName
                })
                .ToListAsync();
        }
        public async Task<LoggedInUserDataDto?> GetLoggedInUserDataDto(Guid UserProfileId)
        {
            var userData = FindByCondition(p => p.UserProfileId == UserProfileId, false)
                .Select(p => new LoggedInUserDataDto
                {
                    UserProfileId = p.UserProfileId,
                    SellerprofileId = p.SellerProfileId != null ? p.SellerProfileId.Value : Guid.Empty,
                    UserName = p.IdentityUser != null ? $"{p.IdentityUser.FirstName} {p.IdentityUser.LastName}" : "No Name",
                    IsAccountConfirmed = p.IdentityUser != null && p.IdentityUser.AccountConfirmed,
                    UserGroupData = p.GroupMemberships.Any()
                        ? p.GroupMemberships.Select(gm => new GroupMemberRolesDto
                        {
                            GroupId = gm.UserGroupId,
                            Role = gm.GroupRole != null ? gm.GroupRole.Description : "Member",
                            GroupName = gm.Group.UserGroupName
                        }).ToList() : new List<GroupMemberRolesDto>()
                })
                .FirstOrDefaultAsync();
            return await userData;

        }

    }
}
   