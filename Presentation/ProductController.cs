using Contracts.Service;

using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;
using Services.Redis;
using Shared.Dtos;
using Shared.RequestFeatures;
using System.Text.Json;

namespace Presentation
{
    [Route("api/products")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {

        private readonly IServiceManager _service;
        private readonly IBackgroundJobQueue _jobQueue;
        private readonly IWebHostEnvironment _env;
        private static readonly Random _random = new();
        private readonly IMemoryCache _cache;






        //private readonly IActiveUserContext _context;

        public ProductController( IMemoryCache cache, IServiceManager service, IBackgroundJobQueue jobQueue, IWebHostEnvironment env)
        {
            _service = service;
            _jobQueue = jobQueue;
            _env = env;
            _cache = cache;
       

            //_context = context;
        }
        private static decimal ParseDecimal(JToken? token)
        {
            if (token is null || token.Type == JTokenType.Null)
                return 0;
            return decimal.TryParse(token.ToString(),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out var d) ? d : 0;
        }

        private static int ParseInt(JToken? token)
        {
            if (token is null || token.Type == JTokenType.Null)
                return 0;
            return int.TryParse(token.ToString(),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out var n) ? n : 0;
        }


        [HttpGet("searchproducts")]
        public  IActionResult Search([FromQuery] string query)
        {
            //var results = await _searchService.Search(query);
            //return Ok(results);
            return Ok(new { message = "Search endpoint is under development" });
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HomePageProductDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts(
       [FromQuery] ProductRequestParameters parameters,CancellationToken cancellationToken)
        {
            var result = await _service.ProductService
                .GetHomePageProductsAsync(parameters, cancellationToken);

            Response.Headers.Append(
                "X-Pagination",
                System.Text.Json.JsonSerializer.Serialize(result.MetaData, _jsonOptions));

            return Ok(result.productsData);
        }


        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        [HttpGet("index")]
        public async Task<ActionResult> HomePageProducts([FromQuery] ProductRequestParameters parameters)
        {
            var products = await _service.ProductService.GetHomePageProductsAsync(parameters);
            var pagination = System.Text.Json.JsonSerializer.Serialize(products.MetaData);
            Response.Headers.Append("X-Pagination", pagination);
            return Ok(products.productsData);
        }



        [HttpGet("{id:Guid}", Name = "ProductById")]
        public async Task<IActionResult> GetProductData(Guid id)
        {
            var product = await _service.ProductService.GetProductDataAsync(id);
            return Ok(product);
        }

        [HttpGet("{slugName}", Name = "ProductBySlugName")]
        public async Task<IActionResult> GetProductDataBySlugName(string slugName)
        {
            var product = await _service.ProductService.GetProductDataBySlugNameAsync(slugName);
            return Ok(product);
        }

        [HttpPost("makeproductfeatured")]
        public async Task<IActionResult> ToggleProductFeaturedState([FromBody] MiniProductImageDto imageDto )
        {
            if(imageDto==null || imageDto.ProductId == Guid.Empty)
            {
                return BadRequest(new { message = "Product Id is required" });
            }
            await _service.ProductService.ToggleProductFeaturedState(imageDto.ProductId);
            return Ok(new { message="success" });
        }

        [HttpPost("makeproductsfeatured")]
        public async Task<ActionResult> MakeProductsFeatured()
        {
            await _service.ProductService.MakeAllProductsFeatured();
            return Ok(new { message = "success" });
        }

        [HttpGet("sellerproduct/{id:Guid}", Name = "SellerProductById")]
        public async Task<ActionResult> GetsellerProduct(Guid id)
        {
            var product = await _service.ProductService.FindSellerProductAsync(id);
            return Ok(product);
        }
        [HttpGet("sellerproductBySlug/{slug}", Name = "SellerProductBySlug")]
        public async Task<ActionResult> GetsellerProductBySlug(string slug)
        {
            var product = await _service.ProductService.FindSellerProductBySlugAsync(slug);
            return Ok(product);
        }

        

        [HttpPost("updateproductprice")]
        public async Task<ActionResult> UpdateProductPrice([FromBody] MiniProductUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest(new { message = "Object is Empty" });

            }
            //var decimalPrice = Convert.ToDecimal(price);
            if (updateDto.ProductId == Guid.Empty)
            {
                return BadRequest(new { message = "Product Id is required" });
            }
             await _service.ProductService.UpdateProductPriceAsync(updateDto.ProductId, updateDto.ProductPrice);
            return Ok(new { message = "Product updated" });
        }

        [HttpPost("setproductoldprice")]
        public async Task<ActionResult> SetProductOldPrice([FromBody] MiniProductUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest(new { message = "Object is Empty" });

            }
            if (updateDto.ProductId == Guid.Empty)
            {
                return BadRequest( new { message = "Product Id is required" });

            }
            await _service.ProductService.SetProductOldPriceAsync(updateDto.ProductId, updateDto.ProductPrice);
            return Ok(new { message = "Product updated" });
        }



        [HttpPost("updateproductstock")]
        public  ActionResult UpdateProductStockQuantity([FromBody] MiniProductUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest(new { message = "Object is Empty" });

            }           
            return Ok(new { message = "Product updated" });

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
     [FromForm] string payload,
     [FromForm] List<IFormFile> images)
        {
            // --- Validate inputs ---
            if (string.IsNullOrWhiteSpace(payload))
                return BadRequest(new { message = "Payload is required." });

            if (images is null || images.Count == 0)
                return BadRequest(new { message = "Product image is required." });

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

            //var price = jsonNode["price"]?.Value<decimal>() ?? 0;
            var price = ParseDecimal(jsonNode["price"]);

            var oldPriceRate = Math.Round(1m + (decimal)_random.NextDouble(), 1);

            var productDto = new NewProductDto
            {
                ProductName = jsonNode["productName"]?.Value<string>() ?? string.Empty,
                Description = jsonNode["description"]?.Value<string>() ?? string.Empty,
                Condition = jsonNode["condition"]?.Value<string>() ?? string.Empty,
                Price = price,
                //CategoryId = jsonNode["categoryId"]?.Value<int>() ?? 0,
                //SubCategoryId = jsonNode["subCategoryId"]?.Value<int>() ?? 0,
                //SubCategoryCategoryId = jsonNode["subCategoryCategoryId"]?.Value<int>() ?? 0,
                CategoryId = ParseInt(jsonNode["categoryId"]),
                SubCategoryId = ParseInt(jsonNode["subCategoryId"]),
                SubCategoryCategoryId = ParseInt(jsonNode["subCategoryCategoryId"]),
                SellerId = sellerId,
                OldPrice = Math.Abs(price * oldPriceRate),
                HasImage = true,
            };

            // --- Filter usable files ---
            var validImages = images.Where(f => f.Length > 0).ToList();
            if (validImages.Count == 0)
                return BadRequest(new { message = "Product image is required." });

            // --- Write all images to temp disk in parallel ---
            // Each write happens concurrently rather than awaiting one after another.
            // Memory stays low (no buffering full bytes), and disk is durable across
            // app pool recycles so the background worker can resume if needed.
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

            // --- Create the product ---
            var product = await _service.ProductService.CreateProductAsync(productDto);

            // --- Build ProductImage rows and enqueue background jobs ---
            var productImages = new List<ProductImage>(savedFiles.Length);
            foreach (var (imageId, tempPath) in savedFiles)
            {
                productImages.Add(new ProductImage
                {
                    ProductImageId = imageId,
                    ProductId = product.ProductId,
                    Folder = $"images/products/{product.ProductId}",
                    FileName = imageId.ToString(),
                    IsPrimary = false,
                    IsProcessed = false,
                    CreatedAt = DateTime.UtcNow,
                });

                _jobQueue.Enqueue(new ImageJob(imageId, product.ProductId, tempPath));
            }

            await _service.ProductImageService.CreateProductImageListAsync(productImages);

            return Ok(new { data = product.Slug });
        }

        [HttpPost("primary-image")]
        public async Task<ActionResult> MakeImagePrimary([FromBody] MiniProductImageDto miniProduct)
        {
            if (miniProduct.ProductId == Guid.Empty|| miniProduct .ImageId== Guid.Empty) return BadRequest("Not Successful");

             await _service.ProductImageService.MakeImagePrimaryAsync(miniProduct);
            return Ok(new { message = "success" });


        }


        [HttpPost("deleteimage")]
        public async Task<ActionResult> DeleteProductImage([FromBody] MiniProductImageDto updateDto)
        {
            if (updateDto.ImageId == Guid.Empty|| updateDto.ProductId == Guid.Empty)
            {
                return BadRequest("ProductImageId is required");
            }
            await _service.ProductImageService.DeleteProductImageIdRefAsync(updateDto.ImageId,updateDto.ProductId);
            return Ok(new { message = "Image Deleted" });
        }

    
        
        
        
        [HttpGet("sellershop/{sellerId:Guid}", Name = "SellerShop")]

        public async Task<IActionResult> GetSellerShop([FromRoute] Guid sellerId)
        {
            if (sellerId == Guid.Empty)
            {
                return BadRequest( new { message = "Seller Id not specified" });
            }
            var sellerShop = await _service.SellerProfileService.GetSellerShopDetailsAsync(sellerId);
            return Ok(sellerShop);
        }


        [HttpGet("sellershopBySlug/{sellerslug}", Name = "SellerShopBySlug")]

        public async Task<IActionResult> GetSellerShopBySlug([FromRoute] string sellerslug)
        {
            if (string.IsNullOrWhiteSpace(sellerslug))
            {
                return BadRequest(new { message = "Seller name not specified" });
            }
            var sellerShop = await _service.SellerProfileService.GetSellerShopDetailsBySlugAsync(sellerslug);
            return Ok(sellerShop);
        }
        



        [HttpPost("update-productdescription")]
        public async Task<ActionResult> UpdateProductDescription([FromBody] SharedUpdatesDto sharedUpdates)
        {
            if (sharedUpdates.ItemId == Guid.Empty) return BadRequest(new { message = "Id not specified" });

            await _service.ProductService.UpdateProductDescription(sharedUpdates);
            return Ok(new { message = "success" });


        }



        [HttpDelete("deleteproduct/{productId:Guid}")]
        public async Task<ActionResult> DeleteProductCategory([FromRoute] Guid productId)
        {
            if (productId == Guid.Empty) { return BadRequest(new { message = "Id is not specified" }); }
            await _service.ProductService.DeleteProductAsync(productId);
            return Ok(new { message = "Item Deleted" });
        }



        [HttpPost("addreview")]
        public async Task<ActionResult> CreateProductReview([FromBody] NewReviewDto newReview)
        {
            if (newReview.RefId == Guid.Empty|| newReview.UserProfileId==Guid.Empty) return BadRequest(new { message = "Id not specified" });
            var IsReviewed = await _service.ProductReviewService.IsReviewedByUserAsync(newReview.UserProfileId,newReview.RefId);
            if (IsReviewed)
            {
                await _service.ProductReviewService.UpdateProductReviewAsync(newReview,IsReviewed);
            }
            else
            {


                await _service.ProductReviewService.CreateProductReviewAsync(newReview);
            }
                return Ok(new { message = "success" });
            

        }

        [HttpPost("createproductimpression")]
        public async Task<ActionResult> CreateProductImpression([FromBody] NewImpressionDto newImpression )
        {
            if (newImpression == null || newImpression.ItemId == Guid.Empty)
            {
                return BadRequest(new { message = "Product Id is required" });
            }
            await _service.ProductImpressionService.CreateProductImpressionAsync(newImpression);
            return Ok(new { message = "success" });
        }


        [HttpGet("by-slug/{slug}")]
        public async Task<ActionResult> GetProductBySlug(string slug)
        {
            var product = await _service.ProductService.FindProductBySlugForPreviewAsync(slug);
              // your existing service method

            if (product == null) return NotFound();
            return Ok(product);
        }

    }
    }


