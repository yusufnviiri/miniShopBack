using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Redis;
using Shared.Dtos;
using Shared.RequestFeatures;

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

        public ProductController(IMemoryCache cache, IServiceManager service, IBackgroundJobQueue jobQueue, IWebHostEnvironment env)
        {
            _service = service;
            _jobQueue = jobQueue;
            _env = env;
            _cache = cache;

            //_context = context;
        }
       
        

        [HttpGet]
        public async Task<ActionResult> GetAllProducts([FromQuery]ProductRequestParameters parameters)
        {
       
                     
            var products = await _service.ProductService.GetHomePageProductsAsync( parameters);

            var pagination = System.Text.Json.JsonSerializer.Serialize(products.MetaData);
            Response.Headers.Append("X-Pagination", pagination);
            return Ok(products.productsData);
        }

        [HttpGet("index")]
        public async Task<ActionResult> HomePageProducts([FromQuery] ProductRequestParameters parameters)
        {
           

            var products = await _service.ProductService.GetHomePageProductsAsync(parameters);
            var pagination = System.Text.Json.JsonSerializer.Serialize(products.MetaData);
            Response.Headers.Append("X-Pagination", pagination);
            return Ok(products.productsData);
        }



        [HttpGet("{id:Guid}", Name = "ProductById")]
        public async Task<ActionResult> GetProductData(Guid id)
        {
            var product = await _service.ProductService.GetProductDataAsync(id);
            return Ok(product);
        }

        [HttpPost("makeproductfeatured")]
        public async Task<ActionResult> ToggleProductFeaturedState([FromBody] MiniProductImageDto imageDto )
        {
            if(imageDto==null || imageDto.ProductId == Guid.Empty)
            {
                return BadRequest(new { message = "Product Id is required" });
            }
            await _service.ProductService.ToggleProductFeaturedState(imageDto.ProductId);
            return Ok(new { message="success" });
        }

        [HttpPost("makeproductsfeatured")]
        public ActionResult MakeProductsFeatured()
        {
            _service.ProductService.MakeAllProductsFeautured();
            return Ok(new { message = "success" });
        }

        [HttpGet("sellerproduct/{id:Guid}", Name = "SellerProductById")]
        public async Task<ActionResult> GetsellerProduct(Guid id)
        {
            var product = await _service.ProductService.FindSellerProductAsync(id);
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
        public async Task<IActionResult> CreateProduct([FromForm] string payload, [FromForm] List<IFormFile> images)
        {
            if (string.IsNullOrWhiteSpace(payload))
                return BadRequest( new { message = "Payload is required." });

            if(images.Count == 0)
            {
                return BadRequest(new { message = "Product image is required" });

            }

            JObject jsonNode;
            try
            {
                jsonNode = JObject.Parse(payload);
            }
            catch (JsonReaderException)
            {
                return BadRequest("Invalid JSON payload.");
            }

            // Map basic product info
            var productDto = new NewProductDto
            {
                ProductName = jsonNode["productName"]?.Value<string>() ?? string.Empty,
                Description = jsonNode["description"]?.Value<string>() ?? string.Empty,

                Price = jsonNode["price"]?.Value<decimal>() ?? 0,
                CategoryId = jsonNode["categoryId"]?.Value<int>() ?? 0,
                SubCategoryId = jsonNode["subCategoryId"]?.Value<int>() ?? 0,
                SubCategoryCategoryId = jsonNode["subCategoryCategoryId"]?.Value<int>() ?? 0,
              

            };

            // Parse SellerId safely
            decimal OldPriceRate = Math.Round(1m + (decimal)_random.NextDouble(), 1);

            var sellerIdString = jsonNode["sellerId"]?.Value<string>();
            if (!Guid.TryParse(sellerIdString, out var sellerId))
                return BadRequest("Invalid SellerId.");
            productDto.SellerId = sellerId;
            productDto.OldPrice = Math.Abs(productDto.Price * OldPriceRate);

            // Map attributes
           
            if (images?.Count > 0)
            {
                productDto.HasImage = true;
            }

                // Create product
                var product = await _service.ProductService.CreateProductAsync(productDto);

            // Process images
            if (images?.Count > 0)
            {
                var productImages = new List<ProductImage>();
                foreach (var file in images)
                {
                    if (file.Length == 0) continue;

                    var imageId = Guid.NewGuid();

                    var tempDir = Path.Combine(_env.ContentRootPath, "uploads", "tmp");
                    Directory.CreateDirectory(tempDir);
                    var tempPath = Path.Combine(tempDir, $"{imageId}.bin");


                    await using (var stream = System.IO.File.Create(tempPath))
                        await file.CopyToAsync(stream);

                    _jobQueue.Enqueue(new ImageJob(imageId, product.ProductId, tempPath));


                    ProductImage productImage = new ProductImage()
                    {
                        ProductImageId = imageId,
                        ProductId = product.ProductId,
                        Folder = $"images/products/{product.ProductId}",
                        FileName = imageId.ToString(),
                        IsPrimary = false,
                        IsProcessed = false,
                        CreatedAt = DateTime.UtcNow,
                    };
                    productImages.Add(productImage);
                }

                if (productImages.Count > 0)
                  await _service.ProductImageService.CreateProductImageListAsync(productImages);
            }

            return Ok(product.ProductId);

            //return Ok(new { message = "Product Added" });
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

    }
    }


