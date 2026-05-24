using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Repository.Repos;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        [ProducesResponseType(typeof(IEnumerable<UserProfileDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUserProfiles(
  [FromQuery] UserRequestParameters parameters, CancellationToken cancellationToken)
        {
            var result = await _service.UserProfileService.GetAllUserProfilesAsync(parameters,cancellationToken);

            Response.Headers.Append(
                "X-Pagination",
                System.Text.Json.JsonSerializer.Serialize(result.MetaData, _jsonOptions));

            return Ok(result.usersData);
        }


        [HttpGet("sellers")]
        [ProducesResponseType(typeof(IEnumerable<UserProfileDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSellerUserProfilesAsync(
            [FromQuery] UserRequestParameters parameters,
            CancellationToken cancellationToken)
        {
            var result = await _service.UserProfileService
                .GetSellerUserProfilesAsync(parameters, cancellationToken);

            Response.Headers.Append(
                "X-Pagination",
                JsonSerializer.Serialize(result.MetaData, _jsonOptions));

            return Ok(result.sellersData);
        }



    //    [HttpGet("sellers")]
    //    [ProducesResponseType(typeof(IEnumerable<UserProfileDto>), StatusCodes.Status200OK)]
    //    public async Task<IActionResult> GetSellers(
    //[FromQuery] UserRequestParameters parameters,
    //CancellationToken cancellationToken)
    //    {
    //        var result = await _service.UserProfileService
    //            .GetSellerUserProfilesAsync(parameters, cancellationToken);

    //        Response.Headers.Append(
    //            "X-Pagination",
    //            JsonSerializer.Serialize(result.MetaData, _jsonOptions));

    //        return Ok(result);  // or result.Items if PagedList<T> exposes that
    //    }





        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };




      
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

        [HttpGet("slug/{slug}", Name = "userProfileBySlug")]
        public async Task<ActionResult> GetUserProfileBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return BadRequest("user id is null");

            }
            var userProfile = await _service.UserProfileService.ShowUserProfileBySlugAsync(slug);

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
            //await _service.UserProfileService.CreateUserProfileWithMemberAsync(newMember);


            var groupSlugName =
              await _service.UserProfileService.CreateUserProfileWithMemberAsync(newMember);

            if (string.IsNullOrWhiteSpace(groupSlugName))
            {
                return BadRequest(new
                {
                    message = "Group slug was not generated"
                });
            }

            return Ok(new
            {
                data = groupSlugName
            });
        }

            [HttpPost("addusertogroup")]
        public async Task<IActionResult> AddUserToGroup([FromBody] GroupMemberDto newMember)
        {
            if (newMember is null)
                return BadRequest("Member object is null");

            var groupSlugName =
                await _service.GroupMemberService.CreateGroupMemberAsync(newMember);

            if (string.IsNullOrWhiteSpace(groupSlugName))
            {
                return BadRequest(new
                {
                    message = "Group slug was not generated"
                });
            }

            return Ok(new
            {
                data = groupSlugName
            });
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



        [HttpPost("makegroupmemberseller")]
        public async Task<IActionResult> MakeGroupMemberSeller([FromBody] GroupMemberSellerprofileDto sellerProfile)
        {
            if (sellerProfile is null)
                return BadRequest("object is null");
            sellerProfile.SellerTypeId = 1; // default to individual
            await _service.SellerProfileService.MakeGroupMemberSellerByGroupAdmin(sellerProfile);
            return Ok(new { message = "seller added" });
        }

        [HttpPost("addsellertouserpreferences")]
        public async Task<IActionResult> AddSellerToUserPreferences([FromBody] UserProfileJoinSellerDto userProfileJoin )
        {
            if (userProfileJoin.SellerProfileId == Guid.Empty|| userProfileJoin.UserProfileId  ==Guid.Empty)
                return BadRequest("reference not specified");
            await _service.UserPreferenceService.AddSellerToUserPreferenceAsync(userProfileJoin.SellerProfileId, userProfileJoin.UserProfileId);
            return Ok(new { message = "  added" });
        }
        [HttpPost("newuserpreference")]
        public async Task<IActionResult> NewUserPreference([FromBody] NewUserPreferenceDto userPreferenceDto)
        {
            if (userPreferenceDto == null)
                return BadRequest("reference not specified");
            if (userPreferenceDto.UserProfileId != Guid.Empty)
            {
                await _service.UserPreferenceService.CreateUserPreferenceAsync(userPreferenceDto);
            }
            return Ok(new { message = " added" });
        }

        [HttpGet("userpreferences")]
        public async Task<ActionResult> GetAllUserPreferences()
        {
            var userPreferences = await _service.UserPreferenceService.GetAllUserPreferencesAsync();
            return Ok(userPreferences);
        }
        [HttpGet("userpreferences/{userProfileId}")]
        public async Task<ActionResult> GetUserPreferences([FromRoute] Guid userProfileId)
        {
            var userPreferences = await _service.UserPreferenceService.GetUserPreferencesAsync(userProfileId);
            return Ok(userPreferences);
        }

        [HttpGet("userfollowers/{userProfileId}")]
        public async Task<ActionResult> GetSellersFollowedByUser([FromRoute] Guid userProfileId)
        {
            var userPreferences = await _service.UserPreferenceService.GetSellersFollowedByUserAsync(userProfileId);
            return Ok(userPreferences);
        }
        [HttpGet("userfollowing/{userProfileId}")]
        public async Task<ActionResult> GetUserFollowing([FromRoute] Guid userProfileId)
        {
            var userFollowers = await _service.UserPreferenceService.GetUserFollowingAsync(userProfileId);
            return Ok(userFollowers);
        }

    }
}
