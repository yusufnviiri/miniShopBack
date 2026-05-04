using Contracts.Lucene;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repository.context;
using Shared.Dtos;
using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private readonly IServiceManager _service;
        
        private readonly IProductSearchService _search;




        public SearchController(IServiceManager service, IProductSearchService search)
        {
            _service = service;
            _search = search;
           

            //_roleManager = roleManager;
        }




        //[HttpGet("products")]
        //public IActionResult SearchProducts([FromQuery] ProductSearchRequest request)
        //{
        //    if (request.PageSize is < 1 or > 100)
        //        return BadRequest("PageSize must be between 1 and 100.");

        //    var result = _searchService.Search(request);
        //    return Ok(result);
        //}


      
        [HttpGet("products")]
        public async Task<ActionResult<ProductSearchResult>> Search(
     [FromQuery] ProductSearchRequest request,
     CancellationToken ct)
        {
            var result = await _search.SearchAsync(request, ct);
            return Ok(result);
        }
    }
}

