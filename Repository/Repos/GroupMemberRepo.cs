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
    public class GroupMemberRepo : RepositoryBase<GroupMember>, IGroupMemberRepo
    {
        public GroupMemberRepo(ApplicationDbContext dbcontext) : base(dbcontext)
        {

        }
        public async Task<ShowGroupMemberDto?> GetGroupMemberById(Guid userGroupId, Guid userProfileId, bool tracking)
        {


            //address




            var userData = FindByCondition(k => k.UserGroupId == userGroupId && k.UserProfileId == userProfileId, tracking).Select(p => new ShowGroupMemberDto()
            {
                FirstName = p.UserProfile.IdentityUser.FirstName,
                LastName = p.UserProfile.IdentityUser.LastName,
                Email = p.UserProfile.IdentityUser.Email,
                PhoneNumber = p.UserProfile.IdentityUser.PhoneNumber,
                MemberStatus = p.MemberStatus.Description,
                IdentityUserId = p.UserProfile.IdentityUserId,
                JoinedAt = p.JoinedAt,
                GroupName = p.Group.UserGroupName,
                GroupMemberId = p.GroupMemberId,
                GroupTypeId = p.Group.GroupTypeId,
                UserGroupId = p.UserGroupId,
                UserProfileId = p.UserProfileId,
                GroupRoleId = p.GroupRoleId,
                MemberStatusId = p.MemberStatusId
            }).FirstOrDefaultAsync();


            return await userData; }









        public async Task<IEnumerable<ShowGroupMemberDto>> GetAllGroupMembers()
        {
            return await FindAll(false).Select(x => new ShowGroupMemberDto()
            {
                GroupMemberId = x.GroupMemberId,
                FirstName = x.UserProfile.IdentityUser.FirstName,
                LastName = x.UserProfile.IdentityUser.LastName,
                PhoneNumber = x.UserProfile.IdentityUser.PhoneNumber,
                Email = x.UserProfile.IdentityUser.Email,
                GroupName = x.Group.UserGroupName,
                GroupRole = x.GroupRole.Description,
                JoinedAt = x.JoinedAt,
                MemberStatus = x.MemberStatus.Description
            }).ToListAsync();


        }
        public async Task<GroupMember?> FindGroupMemberById(Guid memberId, bool tracking)
        {
            return await FindByCondition(m => m.GroupMemberId == memberId, tracking).FirstOrDefaultAsync();

        }
        public async Task<GroupMember?> FindGroupMemberByUserProfileId(Guid userProfileId)
        {
            return await FindByCondition(m => m.UserProfileId == userProfileId, false).Select(p=>new GroupMember() {GroupMemberId=p.GroupMemberId,UserGroupId=p.UserGroupId,UserProfileId=p.UserProfileId,GroupRoleId=p.GroupRoleId,MemberStatusId=p.MemberStatusId }).FirstOrDefaultAsync();

        }
        
    

















        public async Task<bool> IsUserInGroup(Guid userProfileId, Guid userGroupId)
        {
            return await FindByCondition(
                m => m.UserProfileId == userProfileId &&
                     m.UserGroupId == userGroupId,
                trackChanges: false
            ).AnyAsync();
        }

        public void CreateGroupMember(GroupMember member )=>CreateBase(member);
        public void UpdateGroupMember(GroupMember member)=>UpdateBase(member);
        public void DeleteGroupMember(GroupMember member) =>DeleteBase(member);
        public async Task<IEnumerable<GroupMemberRolesDto>> GetAllMemberRoles(Guid userProfileId)
        {
            return await FindByCondition(p=>p.UserProfileId==userProfileId,false).Select(k=>new GroupMemberRolesDto() { GroupId=k.UserGroupId,Role=k.GroupRole.Description,GroupName=k.Group.UserGroupName}).ToListAsync();
        }

        public async Task<IReadOnlyList<Guid>> GetGroupMemberProfileIds(Guid groupId)
        {
            return await FindByCondition(p => p.UserGroupId == groupId, false).Select(k => k.UserProfileId).ToListAsync();

        }

    }
}
