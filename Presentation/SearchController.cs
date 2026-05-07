using Contracts.Lucene;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ITradeSearchService _tradeSearch;
        private readonly IUserSearchService _userSearchService;







        public SearchController(IServiceManager service, IProductSearchService search, ITradeSearchService tradeSearch, IUserSearchService userSearchService)
        {
            _service = service;
            _search = search;
            _tradeSearch = tradeSearch;
            _userSearchService = userSearchService;
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




        [HttpGet("trades")]
        public async Task<ActionResult<TradeSearchResult>> Search(
        [FromQuery] TradeSearchRequest request,
        CancellationToken ct)
        {
            var result = await _tradeSearch.SearchAsync(request, ct);
            return Ok(result);
        }
        /// <summary>
        /// POST /api/search/trades/rebuild
        /// Walks all active trades in SQL and re-indexes them.
        /// Gate behind admin auth in production.
        /// </summary>
        [HttpPost("trades/rebuild")]
        public async Task<IActionResult> Rebuild(
            [FromServices] ApplicationDbContext db,
            [FromServices] TradeDocumentMapper mapper,
            [FromServices] ITradeSearchRepository repo,
            [FromServices] ILuceneIndexRegistry registry,
            CancellationToken ct)
        {
            var ids = await db.Trades
                .AsNoTracking()
                .Where(t => !t.IsDeleted && t.IsActive)
                .Select(t => t.TradeId)
                .ToListAsync(ct);

            var indexed = 0;

            foreach (var id in ids)
            {
                var trade = await db.Trades
                    .AsNoTracking()
                    .Include(t => t.SellerProfile)
                    .Include(t => t.Category)
                    .Include(t => t.SubCategory)
                    .Include(t => t.SubCategoryCategory)
                    .Include(t => t.Images)
                    .FirstOrDefaultAsync(t => t.TradeId == id, ct);

                if (trade is null) continue;

                var agg = await db.TradeReviews
                    .AsNoTracking()
                    .Where(r => r.TradeId == id)
                    .GroupBy(r => 1)
                    .Select(g => new
                    {
                        Count = g.Count(),
                        Avg = (float)g.Average(r => (double)r.Rating)
                    })
                    .FirstOrDefaultAsync(ct);

                var dto = mapper.Map(trade, agg?.Count ?? 0, agg?.Avg ?? 0f);
                repo.AddOrUpdate(dto);
                indexed++;

                if (indexed % 200 == 0) repo.Commit();
            }

            repo.Commit();
            registry.Get(SearchIndexNames.Trades).MaybeRefresh();

            return Ok(new { indexed });
        }

        [HttpGet("users")]
        public async Task<ActionResult<UserSearchResult>> Search(
       [FromQuery] UserSearchRequest request,
       CancellationToken ct)
        {
            var result = await _userSearchService.SearchAsync(request, ct);
            return Ok(result);
        }

        /// <summary>
        /// Walks all user profiles and re-indexes them.
        /// Heavy operation — gate behind admin role in production.
        /// </summary>
        [HttpPost("users/rebuild")]
        //[Authorize(Roles = "Admin")]    // tighten for rebuild
        public async Task<IActionResult> Rebuild(
            [FromServices] ApplicationDbContext db,
            [FromServices] UserDocumentMapper mapper,
            [FromServices] IUserSearchRepository repo,
            [FromServices] ILuceneIndexRegistry registry,
            CancellationToken ct)
        {
            var ids = await db.UserProfiles
                .AsNoTracking()
                .Select(p => p.UserProfileId)
                .ToListAsync(ct);

            var indexed = 0;

            foreach (var id in ids)
            {
                var profile = await db.UserProfiles
                    .AsNoTracking()
                    .Include(p => p.IdentityUser)
                    .Include(p => p.Address)
                    .Include(p => p.SellerProfile)
                    .FirstOrDefaultAsync(p => p.UserProfileId == id, ct);

                if (profile is null) continue;

                var groupNames = await db.GroupMembers
                    .AsNoTracking()
                    .Where(gm => gm.UserProfileId == id)
                    .Select(gm => gm.Group.UserGroupName)
                    .ToListAsync(ct);

                var productCount = profile.SellerProfileId.HasValue
                    ? await db.Products.AsNoTracking().CountAsync(
                        p => p.SellerProfileId == profile.SellerProfileId.Value
                          && p.IsActive && !p.IsDeleted, ct)
                    : 0;

                var tradeCount = profile.SellerProfileId.HasValue
                    ? await db.Trades.AsNoTracking().CountAsync(
                        t => t.SellerProfileId == profile.SellerProfileId.Value
                          && t.IsActive && !t.IsDeleted, ct)
                    : 0;

                var dto = mapper.Map(profile, groupNames,
                    groupNames.Count, productCount, tradeCount);

                repo.AddOrUpdate(dto);
                indexed++;

                if (indexed % 200 == 0) repo.Commit();
            }

            repo.Commit();
            registry.Get(SearchIndexNames.Users).MaybeRefresh();

            return Ok(new { indexed });
        }

    }
}

