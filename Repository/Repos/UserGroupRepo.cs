using Contracts.Repo;
using Entities.Exceptions;
using Entities.Models;
using Lucene.Net.Store;
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
  public class UserGroupRepo : RepositoryBase<UserGroup>, IUserGroupRepo
    {
        private readonly ApplicationDbContext context;
        public UserGroupRepo(ApplicationDbContext _db) : base(_db)
        {
            context = _db;
        }

        public async Task<PagedList<ShowUserGroupDto>> GetUserGroups(
         GeneralRequestParameters request,
         CancellationToken cancellationToken = default)
        {
            var query = FindAll(trackChanges: false).OrderByDescending(k=>k.IsPinned);

            // Ordering — always applied, with a stable tie-breaker.
            // Default to newest-first; UserGroupName as a secondary sort.
            query = request.OrderBy switch
            {
                "name" => query.OrderBy(g => g.UserGroupName)
                                    .ThenBy(g => g.UserGroupId),
                "name-desc" => query.OrderByDescending(g => g.UserGroupName)
                                    .ThenBy(g => g.UserGroupId),
                "members" => query.OrderByDescending(g => g.Members.Count())
                                    .ThenBy(g => g.UserGroupId),
                _ => query.OrderByDescending(g => g.CreatedAt)
                                    .ThenBy(g => g.UserGroupId),
            };

            // Count BEFORE paging so PagedList metadata is correct
            var totalCount = await query.CountAsync(cancellationToken);

            var groups = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(g => new ShowUserGroupDto
                {
                    UserGroupId = g.UserGroupId,
                    UserGroupName = g.UserGroupName,
                    UserGroupSlugName = g.Slug,
                    Contact = g.Contact,
                    Email = g.Email,
                    AboutGroup = g.AboutGroup,
                    GroupType = g.GroupType != null ? g.GroupType.Description : null,
                    City = g.Address != null ? g.Address.City : null,
                    Region = g.Address != null ? g.Address.Region : null,
                    Country = g.Address != null ? g.Address.Country : null,
                    Company = g.Address != null ? g.Address.Company : null,
                    MemberCount = g.Members.Count(),
                })
                .ToListAsync(cancellationToken);

            return new PagedList<ShowUserGroupDto>(
                groups, totalCount, request.PageNumber, request.PageSize);
        }



        public async Task ToggleUserGroupIsPinnedState(Guid userGroupId)
        {
            var rowsAffected = await FindByCondition(p => p.UserGroupId == userGroupId, true)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(
                        p => p.IsPinned,
                        p => !p.IsPinned
                    ));

        

            if (rowsAffected == 0)
            {
                throw new ObjectBadRequestExeption(
                    $"group with id {userGroupId} not found");
            }
           
        }









        public async Task<ShowUserGroupDto?> GetUserGroupById(Guid userGroupId)
        {
            return await FindByCondition(g => g.UserGroupId == userGroupId, false).Select(g => new ShowUserGroupDto
            {
                UserGroupId = g.UserGroupId,
                UserGroupName = g.UserGroupName,
                Contact = g.Contact,
                Email = g.Email,
                GroupType = g.GroupType.Description,
                City = g.Address.City,
                Region = g.Address.Region,
                Country = g.Address.Country,
                Company = g.Address.Company,
                GroupTypeId=g.GroupTypeId,
                AddressId = g.AddressId,
                UserGroupSlugName = g.Slug,
                AboutGroup = g.AboutGroup,            

                MemberCount = g.Members.Count()
            }).FirstOrDefaultAsync();
        }

        public async Task<ShowuserGroupWithMembersDto?> GetUserGroupWithMembers(Guid userGroupId)
        {
            return await FindByCondition(
                    g => g.UserGroupId == userGroupId,
                    trackChanges: false)
                .Select(g => new ShowuserGroupWithMembersDto
                {
                    UserGroupId = g.UserGroupId,
                    UserGroupName = g.UserGroupName,
                    AboutGroup = g.AboutGroup,
                    UserGroupSlugName = g.Slug,

                    City = g.Address != null
                        ? g.Address.City
                        : "unknown",

                    Region = g.Address != null
                        ? g.Address.Region
                        : "unknown",

                    Country = g.Address != null
                        ? g.Address.Country
                        : "unknown",

                    Company = g.Address != null
                        ? g.Address.Company
                        : "unknown",

                    Contact = g.Contact,

                    GroupType = g.GroupType != null
                        ? g.GroupType.Description
                        : "No description",

                    MemberCount = g.Members.Count(),

                    Members = g.Members
                        .Select(m => new ShowGroupMemberDto
                        {
                            FirstName = m.UserProfile.IdentityUser.FirstName,
                            LastName = m.UserProfile.IdentityUser.LastName,
                            Email = m.UserProfile.IdentityUser.Email,
                            PhoneNumber = m.UserProfile.IdentityUser.PhoneNumber,
                            SlugName = m.UserProfile.Slug,

                            GroupRole = m.GroupRole != null
                                ? m.GroupRole.Description
                                : string.Empty,

                            MemberStatus = m.MemberStatus != null
                                ? m.MemberStatus.Description
                                : string.Empty,

                            JoinedAt = m.JoinedAt,
                            UserProfileId = m.UserProfileId,
                            GroupMemberId = m.GroupMemberId,

                            IsSeller = context.GroupSellers
                                .Any(gs =>
                                    gs.GroupMemberId == m.GroupMemberId &&
                                    gs.UserGroupId == g.UserGroupId)
                        })
                        .ToList()
                })
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }
        public async Task<UserGroup?> FindUserGroupById(Guid userGroupId, bool tracking)=>await FindByCondition(g=>g.UserGroupId==userGroupId,tracking).FirstOrDefaultAsync();
        public Guid CreateUserGroup(UserGroup userGroup)
        {
            CreateBase(userGroup);
            return userGroup.UserGroupId;
        }
        public Task<int> GetGroupAddress(Guid userGroupId)
        {
            var addressId = FindByCondition(g => g.UserGroupId == userGroupId, false)
                .Select(g => g.AddressId)
                .FirstOrDefault();
            return Task.FromResult(addressId);
        }
        public async Task<bool> IsGroupMember(Guid userProfileId, Guid userGroupId)
        {
            var isMember =  await FindByCondition(g => g.UserGroupId == userGroupId && g.Members.Any(m => m.UserProfileId == userProfileId), false)
                .AnyAsync();
            return isMember;
        }
        public Task<string?>  GetGroupGroupName(Guid userGroupId)=> FindByCondition(p=>p.UserGroupId==userGroupId, false).Select(g=>g.UserGroupName).FirstOrDefaultAsync();
        public Task<string?> GetGroupGroupSlugName(Guid userGroupId) => FindByCondition(p => p.UserGroupId == userGroupId, false).Select(g => g.Slug).FirstOrDefaultAsync();

        public async Task<string> GetGroupSlugNameOnly(Guid userGroupId) =>await FindByCondition(p => p.UserGroupId == userGroupId, false).Select(g => g.Slug).FirstOrDefaultAsync();





        public void UpdateUserGroup(UserGroup userGroup)=>UpdateBase(userGroup);
        public void DeleteUserGroup(UserGroup userGroup)=>DeleteBase(userGroup);
        public Task<int> NumberOfUserGroups() => FindAll(false).CountAsync();

        


        public async Task<long> NextUserGroupSlugNumberAsync()
        {
            var connection = context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            await using var command = connection.CreateCommand();

            command.CommandText = "SELECT NEXT VALUE FOR UserGroupSlugSeq";

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt64(result);
        }


        public Task<Guid> GetUserGroupIdBySlugName(string slug) => FindByCondition(p => p.Slug == slug, false).Select(k => k.UserGroupId).FirstOrDefaultAsync();



    }
}