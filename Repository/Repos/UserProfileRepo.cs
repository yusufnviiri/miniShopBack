using Contracts.Repo;
using Entities.Models;
using Lucene.Net.Index;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class UserProfileRepo : RepositoryBase<UserProfile>, IUserProfileRepo
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
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
                        LastName = p.IdentityUser.LastName,
                        SlugName=p.Slug
                    },

                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }





        public async Task<PagedList<UserProfileDto>> GetSellerUserProfiles(
        UserRequestParameters request,
        CancellationToken cancellationToken = default)
        {
            // 1) Base query: active sellers without a current group
            var query = FindByCondition(
                p => p.SellerProfileId != null
                  && p.SellerProfileId != Guid.Empty
                  && p.SellerProfile != null
                  && p.ActiveGroupId == null,
                trackChanges: false);

            // 2) Ordering — always applied, with a stable tie-breaker
            query = request.OrderBy switch
            {
                "name-desc" => query
                    .OrderByDescending(p => p.IdentityUser.FirstName)
                    .ThenByDescending(p => p.IdentityUser.LastName)
                    .ThenBy(p => p.UserProfileId),
                "newest" => query
                    .OrderByDescending(p => p.CreatedAt)
                    .ThenBy(p => p.UserProfileId),
                _ => query
                    .OrderBy(p => p.IdentityUser.FirstName)
                    .ThenBy(p => p.IdentityUser.LastName)
                    .ThenBy(p => p.UserProfileId),
            };

            // 3) Count BEFORE paging so PagedList metadata is correct
            var totalCount = await query.CountAsync(cancellationToken);

            // 4) Page + project in a single query
            var users = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new UserProfileDto
                {
                    UserProfileId = p.UserProfileId,
                    SlugName = p.Slug,
                    IsSeller = true,   // filter guarantees this
                    SellerSlugName = p.SellerProfile.Slug,
                    CreatedAt = p.CreatedAt,

                    ApplicationUser = new ShowApplicationUserDto
                    {
                        IdentityUserId = p.IdentityUserId,
                        FirstName = p.IdentityUser.FirstName,
                        LastName = p.IdentityUser.LastName,
                        PhoneNumber = p.IdentityUser.PhoneNumber,
                        City = p.Address != null ? p.Address.City : "Not Specified",
                        Country = p.Address != null ? p.Address.Country : "Not Specified",
                        Company = p.SellerProfile.SellerName,
                        SellerId = p.SellerProfileId,
                        SlugName = p.Slug,
                    },
                })
                .ToListAsync(cancellationToken);

            return new PagedList<UserProfileDto>(
                users, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<PagedList<UserProfileDto>> GetAllUserProfiles(
        UserRequestParameters request,
        CancellationToken cancellationToken = default)
        {
            // 1) Build the filtered query (no ordering yet)
            var query = FindAll(trackChanges: false);

            query = request.Filter switch
            {
                "grouped" => query.Where(p => p.GroupMemberships.Any()),
                "with-details" => query.Where(p => p.IdentityUserId != null),
                _ => query,
            };

            // 2) Apply ordering — ALWAYS, before paginating.
            //    "recent" is just a name for the default order here.
            query = request.OrderBy switch
            {
                "name" => query.OrderBy(p => p.IdentityUser.FirstName)
                               .ThenBy(p => p.IdentityUser.LastName),
                _ => query.OrderByDescending(p => p.CreatedAt),
            };

            // 3) Count the filtered set BEFORE paging (one cheap SQL COUNT)
            var totalCount = await query.CountAsync(cancellationToken);

            // 4) Page + project in a single query
            var users = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsSplitQuery()
                .Select(p => new UserProfileDto
                {
                    UserProfileId = p.UserProfileId,
                    SlugName = p.Slug,
                    IsSeller = p.SellerProfileId != null
                                 && p.SellerProfileId != Guid.Empty,
                    CreatedAt = p.CreatedAt,

                    ApplicationUser = new ShowApplicationUserDto
                    {
                        IdentityUserId = p.IdentityUserId,
                        FirstName = p.IdentityUser.FirstName,
                        LastName = p.IdentityUser.LastName,
                        SlugName = p.Slug,
                    },

                    UserGroups = p.GroupMemberships
                        .Select(g => new ShowUserGroupDto
                        {
                            UserGroupId = g.Group.UserGroupId,
                            UserGroupName = g.Group.UserGroupName,
                            City = g.Group.Address.City,
                            Region = g.Group.Address.Region,
                            Country = g.Group.Address.Country,
                            Company = g.Group.Address.Company,
                            UserGroupSlugName = g.Group.Slug,
                        })
                        .ToList(),
                })
                .ToListAsync(cancellationToken);

            return new PagedList<UserProfileDto>(
                users, totalCount, request.PageNumber, request.PageSize);
        }












































































        public async Task<UserProfileDto?> ShowUserProfile(Guid UserProfileId)
        {

            return await FindByCondition(p => p.UserProfileId == UserProfileId, false).Select(p => new UserProfileDto
            {
                UserProfileId = p.UserProfileId,
                SlugName=p.Slug,
                IsSeller = p.SellerProfileId != Guid.Empty && p.SellerProfileId != null ? true : false,

                UserGroups = p.GroupMemberships.Any()
                          ? p.GroupMemberships.Select(g => new ShowUserGroupDto
                          {
                              UserGroupId = g.Group.UserGroupId,
                              UserGroupName = g.Group.UserGroupName,
                              UserGroupSlugName=g.Group.Slug

                          }).ToList()
                          : new List<ShowUserGroupDto>(),

                ApplicationUser = new ShowApplicationUserDto
                {
                    IdentityUserId = p.IdentityUserId,
                    FirstName = p.IdentityUser.FirstName,
                    LastName = p.IdentityUser.LastName,
                    PhoneNumber = p.IdentityUser.PhoneNumber,
                    Email = p.IdentityUser.Email,
                    SlugName=p.Slug
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
                    SlugName=p.Slug,
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

        public async Task<string?> GetUserContact(Guid userProfileId)
        {
            return await FindByCondition(p => p.UserProfileId == userProfileId, false)
                .Select(p => p.IdentityUser != null ? p.IdentityUser.PhoneNumber : "")
                .FirstOrDefaultAsync();
        }

        public Task<int> NumberOfUserProfiles() => FindAll(false).CountAsync();
     
        public async Task<long> NextUserSlugNumberAsync()
        {
            var connection = _context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            await using var command = connection.CreateCommand();

            command.CommandText = "SELECT NEXT VALUE FOR UserSlugSeq";

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt64(result);
        }
        public Task<Guid> GetUserProfileIdBySlugName(string slug) => FindByCondition(p => p.Slug == slug, false).Select(k => k.UserProfileId).FirstOrDefaultAsync();

    }
}
   