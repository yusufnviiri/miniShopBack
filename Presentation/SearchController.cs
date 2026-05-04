using Contracts.Lucene;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repository.context;
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
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<ApplicationUser>_roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMemoryCache _cache;
        private readonly ILuceneSearchService _searchService;
        private readonly IProductIndexSearch _indexService;
        private readonly ApplicationDbContext _db;



        public SearchController(IServiceManager service, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMemoryCache cache, ILuceneSearchService searchService,
                            IProductIndexSearch indexService,
                            ApplicationDbContext db)
        {
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _cache = cache;
            _searchService = searchService;
            _indexService = indexService;
            _db = db;

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
        public IActionResult SearchProducts([FromQuery] string query)
        {
ProductSearchRequest productSearchRequest = new ProductSearchRequest(query);



            var result = _searchService.Search(productSearchRequest);
            return Ok(result);
        }

        // POST /api/search/index/product/{id}   — index a single product
        [HttpPost("index/product/{id:guid}")]
        public async Task<IActionResult> IndexProduct(Guid id)
        {
            var product = await _db.Products
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product is null) return NotFound();

            _indexService.UpdateProduct(product);
            return Ok(new { message = "Product indexed successfully." });
        }

        // DELETE /api/search/index/product/{id} — remove from index
        [HttpDelete("index/product/{id:guid}")]
        public IActionResult RemoveProduct(Guid id)
        {
            _indexService.DeleteProduct(id);
            return Ok(new { message = "Product removed from index." });
        }

        // POST /api/search/index/rebuild — full rebuild (admin only)
        [HttpPost("index/rebuild")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RebuildIndex()
        {
            var products = await _db.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            _indexService.RebuildIndex(products);
            return Ok(new { message = $"Index rebuilt with {products.Count} products." });
        }
    }
}

