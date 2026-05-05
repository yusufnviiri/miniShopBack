using Contracts.Lucene;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repository.context;
using Repository.lucene;
using Services.Lucene;
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


        [HttpGet("count")]
        public IActionResult Count([FromServices] ILuceneIndexRegistry registry)
        {
            var ctx = registry.Get("products");
            var searcher = ctx.SearcherManager.Acquire();
            try
            {
                return Ok(new
                {
                    numDocs = searcher.IndexReader.NumDocs,
                    maxDoc = searcher.IndexReader.MaxDoc,
                });
            }
            finally
            {
                ctx.SearcherManager.Release(searcher);
            }
        }


        [HttpPost("debug-index/{productId:guid}")]
        public async Task<IActionResult> DebugIndex(
    Guid productId,
    [FromServices] ApplicationDbContext db,                     // ← use your DbContext type name
    [FromServices] ProductDocumentMapper mapper,
    [FromServices] IProductSearchRepository repo,
    [FromServices] ILuceneIndexRegistry registry,
    CancellationToken ct)
        {
            var product = await db.Products.AsNoTracking()
                .Include(p => p.SellerProfile)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Include(p => p.SubCategoryCategory)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.ProductId == productId, ct);

            if (product is null) return NotFound("Product not in DB.");

            var dto = mapper.Map(product, reviewCount: 0, averageRating: 0f);
            repo.AddOrUpdate(dto);
            repo.Commit();

            registry.Get("products").MaybeRefresh();

            var searcher = registry.Get("products").SearcherManager.Acquire();
            try
            {
                return Ok(new
                {
                    indexed = product.ProductName,
                    numDocs = searcher.IndexReader.NumDocs,
                });
            }
            finally
            {
                registry.Get("products").SearcherManager.Release(searcher);
            }
        }


        // Presentation/Controllers/ProductSearchController.cs (admin-gated in production)
        [HttpPost("rebuild")]
        public async Task<IActionResult> Rebuild(
            [FromServices] ApplicationDbContext db,
            [FromServices] ProductDocumentMapper mapper,
            [FromServices] IProductSearchRepository repo,
            [FromServices] ILuceneIndexRegistry registry,
            CancellationToken ct)
        {
            // Stream IDs to avoid loading the whole catalog into memory at once.
            var ids = await db.Products
                .AsNoTracking()
                .Where(p => !p.IsDeleted && p.IsActive)
                .Select(p => p.ProductId)
                .ToListAsync(ct);

            var indexed = 0;

            foreach (var id in ids)
            {
                var product = await db.Products
                    .AsNoTracking()
                    .Include(p => p.SellerProfile)
                    .Include(p => p.Category)
                    .Include(p => p.SubCategory)
                    .Include(p => p.SubCategoryCategory)
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.ProductId == id, ct);

                if (product is null) continue;

                var agg = await db.ProductReviews
                    .AsNoTracking()
                    .Where(r => r.ProductId == id)
                    .GroupBy(r => 1)
                    .Select(g => new
                    {
                        Count = g.Count(),
                        Avg = (float)g.Average(r => (double)r.Rating)
                    })
                    .FirstOrDefaultAsync(ct);

                var dto = mapper.Map(product, agg?.Count ?? 0, agg?.Avg ?? 0f);
                repo.AddOrUpdate(dto);
                indexed++;

                // Commit and refresh in batches so memory stays bounded
                if (indexed % 200 == 0)
                {
                    repo.Commit();
                }
            }

            repo.Commit();
            registry.Get("products").MaybeRefresh();

            return Ok(new { indexed });
        }
    }
}

