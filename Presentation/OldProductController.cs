using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    // [Route("api/oldproducts")]
    //[ApiController]
    //public class OldProductController : ControllerBase
    //{

    //    private readonly IServiceManager _service;
    //    //private readonly IActiveUserContext _context;

    //    public OldProductController(IServiceManager service)
    //    {
    //        _service = service;
    //        //_context = context;
    //    }
    //    [HttpGet]
    //    public async Task<ActionResult> GetAllProducts()
    //    {

    //        var products = await _service.ProductService.GetAllProductsAsync();
    //        return Ok(products);

    //    }
    //    [HttpGet("{id:Guid}", Name = "ProductById")]
    //    public async Task<ActionResult> GetProduct(Guid id)
    //    {
    //        var product = await _service.ProductService.FindProductByIdAsync(false, id);
    //        return Ok(product);
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> CreateProduct([FromForm] string payload, [FromForm] List<IFormFile> images)
    //    {
    //        if (string.IsNullOrWhiteSpace(payload))
    //            return BadRequest("Payload is required.");

    //        JObject jsonNode;
    //        try
    //        {
    //            jsonNode = JObject.Parse(payload);
    //        }
    //        catch (JsonReaderException)
    //        {
    //            return BadRequest("Invalid JSON payload.");
    //        }

    //        // Map basic product info
    //        var productDto = new NewProductDto
    //        {
    //            ProductName = jsonNode["productName"]?.Value<string>() ?? string.Empty,
    //            Price = jsonNode["price"]?.Value<decimal>() ?? 0,
    //            StockQuantity = jsonNode["stockQuantity"]?.Value<int>() ?? 0,
    //            CategoryId = jsonNode["categoryId"]?.Value<int>() ?? 0,
    //            SubCategoryId = jsonNode["subCategoryId"]?.Value<int>() ?? 0,
    //            SubCategoryCategoryId = jsonNode["subCategoryCategoryId"]?.Value<int>() ?? 0,
    //            MeasurementUnit = jsonNode["measurementUnit"]?.Value<string>() ?? string.Empty,

    //        };

    //        // Parse SellerId safely
    //        var sellerIdString = jsonNode["sellerId"]?.Value<string>();
    //        if (!Guid.TryParse(sellerIdString, out var sellerId))
    //            return BadRequest("Invalid SellerId.");
    //        productDto.SellerId = sellerId;

    //        // Map attributes
    //        var attributes = jsonNode["attributes"] as JArray;
    //        if (attributes != null)
    //        {
    //            var attributeValues = new List<ProductAttributeValue>();

    //            foreach (var attr in attributes)
    //            {
    //                var categoryAttributeId = attr["categoryAttributeId"]?.Value<int>() ?? 0;
    //                var inputType = attr["inputTypeValue"]?.Value<string>();
    //                ProductAttributeValue productAttributeValue = inputType switch
    //                {
    //                    "number" => await _service.ProductAttributeValueService
    //                        .MapAttributeValueToCategoryAttribute(categoryAttributeId, intValue: attr["value"]?.Value<int>() ?? 0),
    //                    "radio" => await _service.ProductAttributeValueService
    //                        .MapAttributeValueToCategoryAttribute(categoryAttributeId, boolValue: attr["value"]?.Value<bool>() ?? false),
    //                    "text" => await _service.ProductAttributeValueService
    //                        .MapAttributeValueToCategoryAttribute(categoryAttributeId, stringValue: attr["value"]?.Value<string>()),
    //                    "date" => await _service.ProductAttributeValueService
    //                        .MapAttributeValueToCategoryAttribute(categoryAttributeId, dateOnlyValue: attr["value"]?.Value<DateOnly>() ?? default),
    //                    _ => throw new NotSupportedException($"Unsupported AttributeDataType: {inputType}")
    //                };

    //                attributeValues.Add(productAttributeValue);
    //            }

    //            productDto.ProductAttributeValues = attributeValues;
    //        }

    //        // Create product
    //        var product = await _service.ProductService.CreateProductAsync(productDto);

    //        // Process images
    //        if (images?.Count > 0)
    //        {
    //            var productImages = new List<ProductImage>();

    //            foreach (var file in images)
    //            {
    //                if (file.Length == 0) continue;

    //                using var ms = new MemoryStream();
    //                await file.CopyToAsync(ms);

    //                productImages.Add(new ProductImage
    //                {
    //                    ImageData = ms.ToArray(),
    //                    ImageMimeType = file.ContentType,
    //                    ProductId = product.ProductId
    //                });
    //            }

    //            if (productImages.Count > 0)
    //                await _service.ProductImageService.CreateProductImageListAsync(productImages);
    //        }

    //        return Ok(new { message = "Product Added" });
    //    }


    //    //public async Task<IActionResult> CreateProduct([FromForm] string payload, [FromForm] List<IFormFile> images)
    //    //{
    //    //    if (string.IsNullOrWhiteSpace(payload))
    //    //        return BadRequest("Payload is required.");
    //    //    var JsonNode = JObject.Parse(payload);

    //    //    NewProductDto productDto = new NewProductDto();
    //    //    productDto.ProductName = JsonNode?["productName"]?.Value<string>();
    //    //    productDto.SellerId= Guid.Parse(JsonNode?["sellerId"]?.Value<Guid>())
    //    //    productDto.Price = JsonNode?["price"]?.Value<decimal>() ?? 0;
    //    //    productDto.StockQuantity = JsonNode?["stockQuantity"]?.Value<int>() ?? 0;


    //    //    //convert from json 
    //    //    var attributes = JsonNode["attributes"] as JArray;
    //    //    var attributeValues = new List<ProductAttributeValue>();
    //    //    if (attributes != null)
    //    //    {
    //    //        foreach (var attr in attributes)
    //    //        {
    //    //            ProductAttributeValue productAttributeValue = new();
    //    //            var categoryAttributeId = attr["categoryAttributeId"]?.Value<int>() ?? 0;
    //    //            switch (attr["inputTypeValue"]?.Value<string>())
    //    //            {
    //    //                case "number":
    //    //                    productAttributeValue = await _service.ProductAttributeValueService.MapAttributeValueToCategoryAttribute(categoryAttributeId, intValue: attr["value"]?.Value<int>() ?? 0); break;
    //    //                case "radio":
    //    //                    productAttributeValue = await _service.ProductAttributeValueService.MapAttributeValueToCategoryAttribute(categoryAttributeId, boolValue: attr["value"]?.Value<bool>()); break;
    //    //                case "text":
    //    //                    productAttributeValue = await _service.ProductAttributeValueService.MapAttributeValueToCategoryAttribute(categoryAttributeId, stringValue: attr["value"]?.Value<string>()); break;
    //    //                case "date":
    //    //                    productAttributeValue = await _service.ProductAttributeValueService.MapAttributeValueToCategoryAttribute(categoryAttributeId, dateOnlyValue: attr["value"]?.Value<DateOnly>()); break;
    //    //                default: throw new NotSupportedException($"Unsupported AttributeDataType"); ;

    //    //            }

    //    //            attributeValues.Add(productAttributeValue);

    //    //        }

    //    //        productDto.ProductAttributeValues = attributeValues;
    //    //    }
    //    //    // Do something with categoryAttributeId and value











    //    //    var product = await _service.ProductService.CreateProductAsync(productDto);

    //    //    var productImages = new List<ProductImage>();

    //    //    if (images is { Count: > 0 })
    //    //    {
    //    //        foreach (var file in images)
    //    //        {
    //    //            if (file.Length == 0) continue;

    //    //            using var ms = new MemoryStream();
    //    //            await file.CopyToAsync(ms);

    //    //            productImages.Add(new ProductImage
    //    //            {
    //    //                ImageData = ms.ToArray(),
    //    //                ImageMimeType = file.ContentType,
    //    //                ProductId = product.ProductId
    //    //            });
    //    //        }
    //    //    }

    //    //    if (productImages.Count > 0)
    //    //        await _service.ProductImageService.CreateProductImageListAsync(productImages);

    //    //    return Ok(new { message = "Product Added" });
    //    //}











    //}
}
