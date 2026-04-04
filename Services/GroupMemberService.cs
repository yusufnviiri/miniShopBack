using AutoMapper;
using AutoMapper.Execution;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{


    internal sealed class GroupMemberService : IGroupMemberService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GroupMemberService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<GroupMember?> FindGroupMemberByUserProfileIdAsync(Guid userProfileId)=> await _repoManager.GroupMemberRepo.FindGroupMemberByUserProfileId(userProfileId);

        public async Task<IEnumerable<ShowGroupMemberDto>> GetAllGroupMembersAsync() => await _repoManager.GroupMemberRepo.GetAllGroupMembers();
        public async Task<GroupMember?> FindGroupMemberByIdAsync(Guid memberID, bool tracking) => await _repoManager.GroupMemberRepo.FindGroupMemberById(memberID, tracking);
        public async Task CreateGroupMemberAsync(GroupMemberDto groupMember)
        {

            if (groupMember == null  ) return;
            var memberExists = await _repoManager.GroupMemberRepo.IsUserInGroup(groupMember.UserProfileId, groupMember.UserGroupId);
            if (memberExists) return;
            var newMember = _mapper.Map<GroupMember>(groupMember);
            _repoManager.GroupMemberRepo.CreateGroupMember(newMember);
            await _repoManager.SaveRepoDataAsync();
            var user = await _repoManager.UserProfileRepo.FindUserProfileById(groupMember.UserProfileId, true);
            if (user == null) return;
            user.ActiveGroupId = groupMember.UserGroupId;
            _repoManager.UserProfileRepo.UpdateUserProfile(user);
            await _repoManager.SaveRepoDataAsync();

        }
        public async Task UpdateGroupMemberAsync(GroupMemberDto member)
        {
            var groupMember = await _repoManager.GroupMemberRepo.FindGroupMemberById(member.GroupMemberId, true);
            if (groupMember == null)
            {
                return;
            }
            _mapper.Map(member, groupMember);
            _repoManager.GroupMemberRepo.UpdateGroupMember(groupMember);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteGroupMemberAsync(Guid memberID)
        {
            var groupMember = await _repoManager.GroupMemberRepo.FindGroupMemberById(memberID, true);
            if (groupMember == null)
            {
                return;
            }
            _repoManager.GroupMemberRepo.DeleteGroupMember(groupMember);
            await _repoManager.SaveRepoDataAsync();

        }

        public async Task<ShowGroupMemberDto?> GetGroupMemberByIdAsync(Guid userGroupId, Guid userProfileId, bool tracking)
        {
            var member = await _repoManager.GroupMemberRepo.GetGroupMemberById(userGroupId,userProfileId,tracking);
            if (member != null)
            {
                var user = await _userManager.FindByIdAsync(member.IdentityUserId);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var roleName = roles.FirstOrDefault() ?? "Member";
                    member.GroupRole = roleName;
                }
  
            }
            return member;
        }

        public async Task UpdateGroupMemberProfileAsync(ShowGroupMemberDto member)
        {
            var user = await _userManager.FindByIdAsync(member.IdentityUserId);
            if (user == null)
            {
                return;
            }
            user.FirstName = member.FirstName;
            user.LastName = member.LastName;
            user.Email = member.Email;
            user.PhoneNumber = member.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);

            // update role if changed
            var roles = await _userManager.GetRolesAsync(user);
            var currentRole = roles.FirstOrDefault();
           
            if (currentRole != member.GroupRole)
            {
                var newRole = await _roleManager.FindByIdAsync(member.GroupRole);
                if (newRole != null && currentRole != newRole.Name)
                {
                    await _userManager.RemoveFromRoleAsync(user, currentRole);
                    await _userManager.AddToRoleAsync(user, newRole.Name);
                }
            }


            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError($"Failed to update user profile: {errors}");
            }
            var groupMember = await _repoManager.GroupMemberRepo.FindGroupMemberById(member.GroupMemberId, true);
            if (groupMember == null)
            {
                return;
            }
            groupMember.MemberStatusId = member.MemberStatusId;
            _repoManager.GroupMemberRepo.UpdateGroupMember(groupMember);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task<IEnumerable<GroupMemberRolesDto>> GetAllMemberRolesAsync(Guid userProfileId)=> await _repoManager.GroupMemberRepo.GetAllMemberRoles(userProfileId);

    }
}
