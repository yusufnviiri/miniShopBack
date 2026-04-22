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

        [HttpGet("sellertrade/{id:Guid}", Name = "SellerTradeById")]
        public async Task<ActionResult> GetsellerTrade(Guid id)
        {
            var trade = await _service.TradeService.FindSellerTradeAsync(id);
            return Ok(trade);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTrade([FromForm] string payload, [FromForm] List<IFormFile> images)
        {
            if (string.IsNullOrWhiteSpace(payload))
                return BadRequest(new { message = "Reference Data  is required." });

            if (images.Count == 0)
            {
                return BadRequest( new { message = "trade image is required" });
            }

            JObject jsonNode;
            try
            {
                jsonNode = JObject.Parse(payload);
            }
            catch (JsonReaderException)
            {
                return BadRequest(new { message = "Invalid JSON payload." });
            }

            // Map basic product info
            var tradeDto = new NewTradeDto
            {
                TradeName = jsonNode["tradeName"]?.Value<string>() ?? string.Empty,
                Description = jsonNode["description"]?.Value<string>() ?? string.Empty,
                MinimumPrice = jsonNode["minimumPrice"]?.Value<decimal>() ?? 0,              
                CategoryId = jsonNode["categoryId"]?.Value<int>() ?? 0,
                SubCategoryId = jsonNode["subCategoryId"]?.Value<int>() ?? 0,
                SubCategoryCategoryId = jsonNode["subCategoryCategoryId"]?.Value<int>() ?? 0,

            };




            var sellerIdString = jsonNode["sellerId"]?.Value<string>();
            if (!Guid.TryParse(sellerIdString, out var sellerId))
                return BadRequest(new { message = "Invalid SellerId." });
            tradeDto.SellerId = sellerId;

           
            if (images?.Count > 0)
            {
                tradeDto.HasImage = true;
            }

            // Create product
            var trade = await _service.TradeService.CreateTradeAsync(tradeDto);

            // Process images
            if (images?.Count > 0)
            {
                var tradeImages = new List<TradeImage>();
                foreach (var file in images)
                {
                    if (file.Length == 0) continue;

                    var imageId = Guid.NewGuid();

                    var tempDir = Path.Combine(_env.ContentRootPath, "uploads", "tmp");
                    Directory.CreateDirectory(tempDir);
                    var tempPath = Path.Combine(tempDir, $"{imageId}.bin");


                    await using (var stream = System.IO.File.Create(tempPath))
                        await file.CopyToAsync(stream);

                    _jobQueue.Enqueue(new TradeImageJob(imageId, trade.TradeId, tempPath));


                    TradeImage tradeImage = new TradeImage()
                    {
                        TradeImageId = imageId,
                        TradeId = trade.TradeId,
                        Folder = $"images/trades/{trade.TradeId}",
                        FileName = imageId.ToString(),
                        IsPrimary = false,
                        IsProcessed = false,
                        CreatedAt = DateTime.UtcNow,

                    };
                    tradeImages.Add(tradeImage);

                }

                if (tradeImages.Count > 0)
                    await _service.TradeImageService.CreateTradeImageListAsync(tradeImages);
            }
            return Ok(trade.TradeId);

            //return Ok(new { message = "Product Added" });
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

    }
}
