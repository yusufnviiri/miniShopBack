using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Dtos;
using Shared.RequestFeatures;

namespace Presentation
{
    [Route("api/trades")]
    [ApiController]
    //[Authorize]
    public class TradeController : ControllerBase
    {

        private readonly IServiceManager _service;
        private readonly IBackGroundTradeImageJobQueue _jobQueue;
        private readonly IWebHostEnvironment _env;
        private static readonly Random _random = new();


        //private readonly IActiveUserContext _context;

        public TradeController(IServiceManager service, IBackGroundTradeImageJobQueue jobQueue, IWebHostEnvironment env)
        {
            _service = service;
            _jobQueue = jobQueue;
            _env = env;

            //_context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllHomePageTrades([FromQuery] ProductRequestParameters parameters)
        {

            var trades = await _service.TradeService.GetHomePageTradesAsync(parameters);

            var pagination = System.Text.Json.JsonSerializer.Serialize(trades.MetaData);
            Response.Headers.Append("X-Pagination", pagination);
            return Ok(trades.tradersData);
        }

      
            [HttpPost("maketradefeatured")]
        public ActionResult MakeTradeFeatured(Guid id)
        {
            _service.TradeService.MakeTradeFeautured(id);
            return Ok(new { message = "success" });
        }

        [HttpPost("makealltradesfeatured")]
        public ActionResult MakeAllTradesFeatured()
        {
            _service.TradeService.MakeAllTradesFeautured();
            return Ok(new { message = "success" });
        }


        [HttpGet("{id:Guid}", Name = "TradeById")]
        public async Task<ActionResult> GetTradeData(Guid id)
        {
            var trade = await _service.TradeService.GetTradeDataAsync(id);
            return Ok(trade);
        }

        [HttpGet("{slug}", Name = "TradeBySlug")]
        public async Task<ActionResult> GetTradeDataBySlug(string slug)
        {
            var trade = await _service.TradeService.GetTradeDataUsingSlugNameAsync(slug);
            return Ok(trade);
        }

        [HttpGet("sellertrade/{id:Guid}", Name = "SellerTradeById")]
        public async Task<ActionResult> GetsellerTrade(Guid id)
        {
            var trade = await _service.TradeService.FindSellerTradeAsync(id);
            return Ok(trade);
        }

        [HttpGet("sellertradebyslug/{slug}", Name = "SellerTradeBySlug")]
        public async Task<ActionResult> GetsellerTradeBySlug(string slug)
        {
            var trade = await _service.TradeService.FindSellerTradeUsingSlugNameAsync(false,slug);
            return Ok(trade);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrade(
      [FromForm] string payload,
      [FromForm] List<IFormFile> images)
        {
            // --- Validate inputs ---
            if (string.IsNullOrWhiteSpace(payload))
                return BadRequest(new { message = "Reference Data is required." });

            if (images is null || images.Count == 0)
                return BadRequest(new { message = "Trade image is required." });

            // --- Parse payload ---
            JObject jsonNode;
            try
            {
                jsonNode = JObject.Parse(payload);
            }
            catch (JsonReaderException)
            {
                return BadRequest(new { message = "Invalid JSON payload." });
            }

            var sellerIdString = jsonNode["sellerId"]?.Value<string>();
            if (!Guid.TryParse(sellerIdString, out var sellerId))
                return BadRequest(new { message = "Invalid SellerId." });

            var tradeDto = new NewTradeDto
            {
                TradeName = jsonNode["tradeName"]?.Value<string>() ?? string.Empty,
                Description = jsonNode["description"]?.Value<string>() ?? string.Empty,
                MinimumPrice = jsonNode["minimumPrice"]?.Value<decimal>() ?? 0,
                CategoryId = jsonNode["categoryId"]?.Value<int>() ?? 0,
                SubCategoryId = jsonNode["subCategoryId"]?.Value<int>() ?? 0,
                SubCategoryCategoryId = jsonNode["subCategoryCategoryId"]?.Value<int>() ?? 0,
                SellerId = sellerId,
                HasImage = true,
            };

            // --- Filter usable files ---
            var validImages = images.Where(f => f.Length > 0).ToList();
            if (validImages.Count == 0)
                return BadRequest(new { message = "Trade image is required." });

            // --- Write all images to temp disk in parallel ---
            // Each write happens concurrently rather than awaiting one after another.
            // Memory stays low, disk is durable across app pool recycles, and the
            // background worker can resume processing from the temp path.
            var tempDir = Path.Combine(_env.ContentRootPath, "uploads", "tmp");
            Directory.CreateDirectory(tempDir);

            var savedFiles = await Task.WhenAll(validImages.Select(async file =>
            {
                var imageId = Guid.NewGuid();
                var tempPath = Path.Combine(tempDir, $"{imageId}.bin");

                await using var stream = System.IO.File.Create(tempPath);
                await file.CopyToAsync(stream);

                return (ImageId: imageId, TempPath: tempPath);
            }));

            // --- Create the trade ---
            var trade = await _service.TradeService.CreateTradeAsync(tradeDto);

            // --- Build TradeImage rows and enqueue background jobs ---
            var tradeImages = new List<TradeImage>(savedFiles.Length);
            foreach (var (imageId, tempPath) in savedFiles)
            {
                tradeImages.Add(new TradeImage
                {
                    TradeImageId = imageId,
                    TradeId = trade.TradeId,
                    Folder = $"images/trades/{trade.TradeId}",
                    FileName = imageId.ToString(),
                    IsPrimary = false,
                    IsProcessed = false,
                    CreatedAt = DateTime.UtcNow,
                });

                _jobQueue.Enqueue(new TradeImageJob(imageId, trade.TradeId, tempPath));
            }

            await _service.TradeImageService.CreateTradeImageListAsync(tradeImages);

            return Ok(new { data = trade.Slug });
        }

        [Authorize]

        [HttpPost("primary-tradeimage")]
        public async Task<ActionResult> MakeTradeImagePrimary([FromBody] MiniTradeImage miniTradeImage )
        {
            if (miniTradeImage.TradeId == Guid.Empty || miniTradeImage.ImageId == Guid.Empty) return BadRequest(new { message = "Ids not specified" });

            await _service.TradeImageService.MakeImagePrimaryAsync(miniTradeImage);
            return Ok(new { message = "success" });


        }

        [HttpPost("deletetradeimage")]
        public async Task<ActionResult> DeleteTradeImage([FromBody] MiniTradeImage tradeImage )
        {
            if (tradeImage.TradeId == Guid.Empty || tradeImage.ImageId == Guid.Empty)
            {
                return BadRequest(new { message = "Product Image Ref is required" });
            }
            await _service.TradeImageService.DeleteTradeImageIdRefAsync(tradeImage.ImageId, tradeImage.TradeId);
            return Ok(new { message = "Image Deleted" });
     }

        [HttpPost("update-tradedescription")]
        public async Task<ActionResult> UpdateTradeDescription([FromBody] SharedUpdatesDto sharedUpdates )
        {
            if (sharedUpdates.ItemId == Guid.Empty ) return BadRequest(new { message = "Id not specified" });

            await _service.TradeService.UpdateTradeDescription(sharedUpdates);
            return Ok(new { message = "success" });


        }
        [HttpPost("addreview")]
        public async Task<ActionResult> CreateTradeReview([FromBody] NewReviewDto newReview)
        {
            if (newReview.RefId == Guid.Empty || newReview.UserProfileId == Guid.Empty) return BadRequest(new { message = "Id not specified" });
            var IsReviewed = await _service.TradeReviewService.IsReviewedByUserAsync(newReview.UserProfileId, newReview.RefId);
            if (IsReviewed)
            {
                await _service.TradeReviewService.UpdateTradeReviewAsync(newReview, IsReviewed);
            }
            else
            {
                await _service.TradeReviewService.CreateTradeReviewAsync(newReview);
            }
            return Ok(new { message = "success" });

        }


        [HttpPost("createtradeimpression")]
        public async Task<ActionResult> CreateTradeImpression([FromBody] NewImpressionDto newImpression)
        {
            if (newImpression == null || newImpression.ItemId == Guid.Empty)
            {
                return BadRequest(new { message = "Product Id is required" });
            }
            await _service.TradeImpressionService.CreateTradeImpressionAsync(newImpression);
            return Ok(new { message = "success" });
        }

        [HttpDelete("deletetrade/{tradeId:Guid}")]
        public async Task<ActionResult> DeleteTrade([FromRoute] Guid tradeId)
        {
            if (tradeId == Guid.Empty) { return BadRequest(new { message = "Id is not specified" }); }
            await _service.TradeService.DeleteTradeAsync(tradeId);
            return Ok(new { message = "Item Deleted" });
        }

    }
}
