using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation
{


    [Route("api/groups")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {

        private readonly IServiceManager _service;
        public UserGroupController(IServiceManager service)
        {
            _service = service;
        }
     




        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ShowUserGroupDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSellerUserProfilesAsync([FromQuery] GeneralRequestParameters parameters, CancellationToken cancellationToken)
        {
            var result = await _service.UserGroupService.GetUserGroupsAsync(parameters,cancellationToken);

            Response.Headers.Append(
                "X-Pagination",
                System.Text.Json.JsonSerializer.Serialize(result.MetaData, _jsonOptions));

            return Ok(result.groupsData);
        }


        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };














        [HttpGet("{id:Guid}", Name = "userGroupById")]
        public async Task<ActionResult> GetUserGroup(Guid id)
        {
            var group = await _service.UserGroupService.GetUserGroupByIdAsync(id);
            return Ok(group);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUsergroup([FromBody] NewUserGroupDto groupDto)
        {
            if (groupDto is null)
                return BadRequest(new { message = "Item is null" });
            await _service.UserGroupService.CreateUserGroupAsync(groupDto);
            return Ok(new { message = "Group created" });
        }
        [HttpPut("edit")]
        public async Task<ActionResult> UpdateUserGroup(NewUserGroupDto groupDto)
        {
            if (groupDto is null) { return BadRequest("Object is null"); }
            await _service.UserGroupService.UpdateUserGroupAsync(groupDto);
            return Ok(new { message = "Update user Group Successful" });
        }
        [HttpGet("{groupId}/members")]
        public async Task<ActionResult> GetUserGroupMembers([FromRoute] Guid groupId)
        {
            if (groupId == Guid.Empty) { return BadRequest("Id is zero"); }
         var groupData=   await _service.UserGroupService.GetUserGroupWithMembersAsync(groupId);

            return Ok(groupData);
        }


        [HttpGet("{slug}/memberswithslug")]
        public async Task<ActionResult> GetUserGroupMembersWithSlug([FromRoute] string slug )
        {
            if (string.IsNullOrWhiteSpace(slug)) { return BadRequest("Id is zero"); }
            var groupData = await _service.UserGroupService.GetUserGroupWithMembersWithSlugAsync(slug);

            return Ok(groupData);
        }
        

        [HttpGet("{groupId}/members/{userProfileId}")]
        public async Task<ActionResult> GetUserGroupMember([FromRoute] Guid groupId, [FromRoute] Guid userProfileId)
        {
            if (groupId == Guid.Empty|| userProfileId == Guid.Empty) { return BadRequest("Id is zero"); }
            var groupMember = await _service.GroupMemberService.GetGroupMemberByIdAsync(groupId,userProfileId,false);

            return Ok(groupMember);
        }
        [HttpPut("member")]
        public async Task<IActionResult> UpdateGroupMember([FromBody] ShowGroupMemberDto groupDto)
        {
            if (groupDto is null)
                return BadRequest("group object is null");
            await _service.GroupMemberService.UpdateGroupMemberProfileAsync(groupDto);
            return Ok(new { message = "Group updated" });
        }
        [HttpGet("groupshop/{sellerId:Guid}", Name = "groupshop")]
        public async Task<ActionResult> GetGroupShop(Guid sellerId)
        {
            if(sellerId==Guid.Empty) { return BadRequest(new { message = "Group Id not specified" }); }
            var groupshop = await _service.SellerProfileService.GetGroupShopDetails(sellerId,true);
            return Ok(groupshop);
        }


        [HttpGet("groupshopBySlug/{slug}", Name = "groupshopBySlug")]
        public async Task<ActionResult> GetGroupShopBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) { return BadRequest(new { message = "Group slug not specified" }); }
            var groupshop = await _service.SellerProfileService.GetGroupShopDetailsBySlugAsync(slug, true);
            return Ok(groupshop);
        }

        [HttpGet("groupshopdisplay/{sellerId:Guid}", Name = "groupshopdisplay")]
        public async Task<ActionResult> GetGroupShopDisplay(Guid sellerId)
        {
            if (sellerId == Guid.Empty) { return BadRequest(new { message = "Group Id not specified" }); }
            var groupshop = await _service.SellerProfileService.GetGroupShopDisplay(sellerId);
            return Ok(groupshop);
        }
        [HttpGet("groupshopdisplayBySlug/{slug}", Name = "groupshopdisplayBySlug")]
        public async Task<ActionResult> GetGroupShopDisplayBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) { return BadRequest(new { message = "Group slug not specified" }); }
            var groupshop = await _service.SellerProfileService.GetGroupShopDisplayBySlugAsync(slug);
            return Ok(groupshop);
        }



        [HttpDelete("{groupId}/delete")]
        public async Task<ActionResult> DeleteGroup([FromRoute] Guid groupId)
        {
            if (groupId == Guid.Empty) { return BadRequest("Id is zero"); }
            await _service.UserGroupService.DeleteUserGroupAsync(groupId);
            return Ok(new { message = "Group Deleted" });
        }


        //[Authorize]
        //[HttpPost("{groupId}/products")]
        //public async Task<IActionResult> CreateProduct(Guid groupId, CreateProductRequest request)
        //{
        //    var userId = User.GetUserId();

        //    await _groupAuth.EnsureRoleAsync(
        //        userId,
        //        groupId,
        //        GroupRole.Owner,
        //        GroupRole.Admin,
        //        GroupRole.Seller);

        //    // create product
        //    return Ok();
        //}


    }
}
