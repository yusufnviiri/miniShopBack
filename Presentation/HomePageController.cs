using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{


    [Route("api/home")]
    [ApiController]

    public class HomePageController : ControllerBase
    {

        private readonly IServiceManager _service;
        public HomePageController(IServiceManager service)
        {
            _service = service;
        }
        [Authorize]

        [HttpGet]

        public async Task<ActionResult> GetAllHomepageCards()
        {

            var cards = await _service.HomePageCardService.GetAllHomePageCardDtos();
            return Ok(cards);

        }
        [Authorize]

        [HttpGet("{id:int}", Name = "homepagecardId")]
        public async Task<ActionResult> GetHomePageCard(int id)
        {
            var card = await _service.HomePageCardService.FindHomePageCardByIdAsync(id, false);
            if (card == null)
            {
                return NotFound(new { message = "Item Not Found" });
            }
            return Ok(card);
        }
        [Authorize]

        [HttpPost]
        public async Task<IActionResult> CreateHomePageCard([FromBody] HomePageCard homePage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Item is null" });
            }
            else if (homePage is null)
            {
                return NotFound(new { message = "Item Not Found" });
            }
            else
            {
                await _service.HomePageCardService.CreateHomePageCardAsync(homePage);
                return Ok(new { message = "HomePageCard created" });
            }


        }
        [Authorize]

        [HttpPut("edit")]
        public async Task<ActionResult> UpdateHomePageCard(HomePageCard homePageCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Item is null" });
            }
            else if (homePageCard is null)
            {
                return NotFound(new { message = "Item Not Found" });
            }
            await _service.HomePageCardService.UpdateHomePageCardAsync(homePageCard);
            return Ok(new { message = "Update user Group Successful" });
        }
        [Authorize]

        [HttpDelete("card/{id}")]
        public async Task<ActionResult> DeleteProductCategory([FromRoute] int id)
        {
            if (id == 0) { return BadRequest(new { message = "Id is zero" }); }
            await _service.HomePageCardService.DeleteHomePageCardAsync(id);
            return Ok(new { message = "Item Deleted" });
        }

        //groupfeatured products




        [HttpGet("groupfeaturedproducts")]

        public async Task<ActionResult> GetAllGroupFeaturedProducts()
        {

            var products = await _service.GroupFeaturedProductService.GetAllGroupFeaturedProductsAsync();
            return Ok(products);

        }

        [HttpGet("groupfeaturedproductsbyusergroup/{userGroupId:guid}")]
        public async Task<ActionResult> GetGroupFeaturedProductsByUserGroupId(Guid userGroupId)
        {
            var products = await _service.GroupFeaturedProductService.GetGroupFeaturedProductsByUserGroupIdAsync(userGroupId);
            return Ok(products);

        }
        [Authorize]

        [HttpGet("groupfeaturedproduct/{id:int}")]
        public async Task<ActionResult> GetGroupFeaturedProduct(int id)
        {
            var product = await _service.GroupFeaturedProductService.FindGroupFeaturedProductByIdAsync(id, false);
            if (product == null)
            {
                return NotFound(new { message = "Item Not Found" });
            }
            return Ok(product);
        }
        [Authorize]

        [HttpPost("groupfeaturedproduct")]
        public async Task<IActionResult> CreateGroupFeaturedProduct([FromBody] NewGroupFeaturedProductDto groupFeaturedProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Item is null" });
            }
            else if (groupFeaturedProduct.UserGroupId==Guid.Empty|| groupFeaturedProduct.ProductId==Guid.Empty)
            {
                return NotFound(new { message = "Item Id not specified " });
            }
            else
            {
                await _service.GroupFeaturedProductService.CreateGroupFeaturedProductAsync(groupFeaturedProduct);
                return Ok(new { message = "GroupFeaturedProduct created" });
            }


        }
        [Authorize]

        [HttpPut("groupfeaturedproduct")]
        public async Task<ActionResult> UpdateGroupFeaturedProduct(GroupFeaturedProduct groupFeaturedProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Item is null" });
            }
            else if (groupFeaturedProduct.GroupFeaturedProductId==0)
            {
                return NotFound(new { message = "Item Not Found" });
            }
            await _service.GroupFeaturedProductService.UpdateGroupFeaturedProductAsync(groupFeaturedProduct);
            return Ok(new { message = "Update user Group Successful" });
        }
        [Authorize]

        [HttpDelete("groupfeaturedproduct/{id}")]
        public async Task<ActionResult> DeleteGroupFeaturedProduct([FromRoute] int id)
        {
            if (id == 0) { return BadRequest(new { message = "Id is zero" }); }
            await _service.GroupFeaturedProductService.DeleteGroupFeaturedProductAsync(id);
            return Ok(new { message = "Item Deleted" });
        }


        [HttpGet("index")]
        public async Task<ActionResult> HomePageProducts()
        {
            var products = await _service.ProductService.HomePageCustomProductsAsync();
            return Ok(products);

        }
    }
}
