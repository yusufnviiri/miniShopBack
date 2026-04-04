using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Presentation
{
  
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IServiceManager _service;
        public UserController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllUserProfiles()
        {
            var users = await _service.UserProfileService.GetAllUserProfilesAsync();
            return Ok(users);
        }
        [HttpGet("sellers")]
        public async Task<ActionResult> GetSellerUserProfiles()
        {
            var users = await _service.UserProfileService.GetSellerUserProfilesAsync();
            return Ok(users);
        }
        [HttpGet("withoutgroups")]
        public async Task<ActionResult> GetUsersWithoutGroup()
        {
            var users = await _service.UserProfileService.GetAllUserProfilesWithoutGroupsAsync();
            return Ok(users);
        }


        [HttpGet("{id:Guid}", Name = "userProfileById")]
        public async Task<ActionResult> GetUserProfile(Guid id)
        {
            var userProfile = await _service.UserProfileService.ShowUserProfileAsync(id);
            return Ok(userProfile);
        }

        [Authorize]

        [HttpPost]
        public async Task<IActionResult> CreateUserProfile([FromBody] NewUserDataDto newUser )
        {
            if (newUser is null)
                return BadRequest("user object is null");
            await _service.UserProfileService.CreateUserProfileAsync(newUser);
            return Ok(new { message = "user profile created" });
        }
        [HttpPut("edit")]
        public async Task<ActionResult> UpdateUserProfile(NewUserDataDto userDataDto  )
        {
            if (userDataDto is null) { return BadRequest("Object is null");}
            
            await _service.UserProfileService.UpdateUserProfileAsync(userDataDto);
            return Ok(new { message = "Update user Group Successful" });
        }
        [HttpDelete("{profileId}")]
        public async Task<ActionResult> DeleteUserProfile([FromRoute] Guid profileId)
        {
            if (profileId == Guid.Empty) { return BadRequest("Id is zero"); }
            await _service.UserProfileService.DeleteUserProfileAsync(profileId);
            return Ok(new { message = "Profile Deleted" });
        }
        [HttpPost("groupmember")]
        public async Task<IActionResult> AddGroupMember([FromBody] GroupMemberJoinNewUserProfileDataDto newMember)
        {
            if (newMember is null)
                return BadRequest("member object is null");
            await _service.UserProfileService.CreateUserProfileWithMemberAsync(newMember);
            return Ok(new { message = "group member added" });
        }

            [HttpPost("addusertogroup")]
            public async Task<IActionResult> AddUserToGroup([FromBody] GroupMemberDto newMember)
            {
                if (newMember is null)
                    return BadRequest("member object is null");
                await _service.GroupMemberService.CreateGroupMemberAsync(newMember);
                return Ok(new { message = "group member added" });
            }
            [HttpPut("groupmember/edit")]
        public async Task<ActionResult> UpdateGroupMember(GroupMemberDto groupMember )
        {
            if (groupMember is null) { return BadRequest("Object is null"); }

            await _service.GroupMemberService.UpdateGroupMemberAsync(groupMember);
            return Ok(new { message = "Update  Group Member Successful" });
        }
        [HttpDelete("groupmember/{memberId}")]
        public async Task<ActionResult> DeleteGroupMember([FromRoute] Guid memberId)
        {
            if (memberId == Guid.Empty) { return BadRequest("Id is zero"); }
            await _service.GroupMemberService.DeleteGroupMemberAsync(memberId);
            return Ok(new { message = "Member Deleted" });
        }
        [HttpPost("makeuserseller")]
        public async Task<IActionResult> MakeUserSeller([FromBody] SellerProfile sellerProfile )
        {
            if (sellerProfile is null)
                return BadRequest("object is null");
            sellerProfile.SellerTypeId = 1; // default to individual
            await _service.SellerProfileService.CreateSellerProfile(sellerProfile);
            return Ok(new { message = "seller added" });
        }
    }
}
