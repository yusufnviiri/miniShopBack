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
  public class UserGroupRepo : RepositoryBase<UserGroup>, IUserGroupRepo
    {
        public UserGroupRepo(ApplicationDbContext _db) : base(_db)
        {

        }

       public async Task<IEnumerable<ShowUserGroupDto>> GetUserGroups()
        {
            return await FindAll(false)
                .Select(g => new ShowUserGroupDto
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
                    AboutGroup=g.AboutGroup,
                    UserGroupSlugName=g.Slug,
                    
                    MemberCount = g.Members.Count()
                }).ToListAsync();
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
            return await FindByCondition(g => g.UserGroupId == userGroupId, trackChanges: false)
                .Select(g => new ShowuserGroupWithMembersDto
                {
                    UserGroupId = g.UserGroupId,
                    UserGroupName = g.UserGroupName,
                    AboutGroup = g.AboutGroup,
                    UserGroupSlugName = g.Slug,
                    City = g.Address!=null?g.Address.City:"unkown",
                    Region = g.Address != null ? g.Address.Region:"unkown",
                    Country = g.Address != null ? g.Address.Country : "unkown"  ,
                    Company = g.Address != null ? g.Address.Company : "unkown",
                    

                    Contact = g.Contact,
                    GroupType =g.GroupType!=null? g.GroupType.Description:"No description",

                    MemberCount = g.Members.Count(),

                    Members = g.Members.Select(m => new ShowGroupMemberDto
                    {
                        FirstName = m.UserProfile.IdentityUser.FirstName,
                        LastName = m.UserProfile.IdentityUser.LastName,
                        Email = m.UserProfile.IdentityUser.Email,
                        PhoneNumber = m.UserProfile.IdentityUser.PhoneNumber,
                        SlugName=m.UserProfile.Slug,

                        GroupRole = m.GroupRole != null
                            ? m.GroupRole.Description
                            : string.Empty,

                        MemberStatus = m.MemberStatus != null
                            ? m.MemberStatus.Description
                            : string.Empty,

                        JoinedAt = m.JoinedAt,
                        UserProfileId = m.UserProfileId,

                        IsSeller = m.GroupSellers!=null? m.GroupSellers.Any(p => p.UserGroupId == userGroupId):false
                    }).ToList()
                })
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

        public Task<Guid> GetUserGroupIdBySlugName(string slug) => FindByCondition(p => p.Slug == slug, false).Select(k => k.UserGroupId).FirstOrDefaultAsync();



    }
}