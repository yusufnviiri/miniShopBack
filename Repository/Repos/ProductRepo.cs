using Contracts.Repo;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repository.context;
using Shared.Dtos;
using Shared.RequestFeatures;


namespace Repository.Repos
{

    public class ProductRepo : RepositoryBase<Product>, IProductRepo
    {
        private readonly IMemoryCache _cache;

        public ProductRepo(ApplicationDbContext _db, IMemoryCache cache) : base(_db)
        {
            _cache = cache;

        }

        public void MakeAllProductsFeautured()
        {
            var query = FindAll(false).Where(p => !p.IsFeatured).ExecuteUpdateAsync(setters => setters.SetProperty(p => p.IsFeatured, true));      
        }

        public void MakeProductFeautured(Guid productId)
        {
            var query = FindByCondition(p => p.ProductId == productId, false).ExecuteUpdateAsync(setters => setters.SetProperty(p => p.IsFeatured, true));

        }

        public async Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProducts(bool tracking)
        {
            var query = FindAll(tracking);
            return await query
       .AsSplitQuery()
       .Select(x => new ShowProductMiniDetailsDto
       {
           ProductId = x.ProductId,
           ProductName = x.ProductName,
           Price = x.Price,
           CategoryName = x.Category != null ? x.Category.CategoryName : "General",
           SellerProfileId = x.SellerProfileId,
           SellerName = x.SellerProfile != null ? $"{x.SellerProfile.SellerName}" : "Unkown Seller",

       }).ToListAsync();
        }

       // public async Task<PagedList<HomePageProductDto>> GetHomePageProducts([FromQuery]
       //ProductRequestParameters requestParameters)
       // {
       //     //var indivisualProducts = FindByCondition(p => p.SellerProfile!=null? p.SellerProfile.SellerTypeId == 1:false, false);

        
       //     var query = FindAll(false)
       //         .Where(p => p.IsActive && !p.IsDeleted && p.SellerProfile != null ? p.SellerProfile.SellerTypeId == 1:true );

       //     // 🔎 Filter by Category
       //     if (requestParameters.CategoryId.HasValue && requestParameters.CategoryId.Value > 0)
       //     {
       //         var category = requestParameters.CategoryId;
       //         query = query.Where(p =>
       //             p.Category != null &&
       //             p.Category.CategoryId == category);
       //     }

       //     // 🔎 Filter by Manufacturer
       //     if (!string.IsNullOrWhiteSpace(requestParameters.Manufacturer))
       //     {
       //         var name = requestParameters.Manufacturer.Trim().ToLower();
       //         query = query.Where(p => p.Manufacturer.Contains(name, StringComparison.CurrentCultureIgnoreCase));
       //     }


       //     // 🔎 Filter by SubCategory
       //     if (requestParameters.SubCategoryId.HasValue && requestParameters.SubCategoryId.Value > 0)
       //     {

       //         var subCategory = requestParameters.SubCategoryId;
       //         query = query.Where(p =>
       //             p.SubCategory != null &&
       //             p.SubCategory.SubCategoryId == subCategory);

       //     }
       //     // 💰 Price Filtering
       //     if (requestParameters.MinPrice.HasValue)
       //     {
       //         query = query.Where(p => p.Price >= requestParameters.MinPrice.Value);
       //     }

       //     if (requestParameters.MaxPrice.HasValue)
       //     {
       //         query = query.Where(p => p.Price <= requestParameters.MaxPrice.Value);
       //     }

       //     // 🔤 Product Name Search
       //     if (!string.IsNullOrWhiteSpace(requestParameters.ProductName))
       //     {
       //         var name = requestParameters.ProductName.Trim().ToLower();
       //         query = query.Where(p => p.ProductName.ToLower().Contains(name));
       //     }

       //     query = query.OrderByDescending(p => p.CreatedAt);

       //     var count = await query.CountAsync();

       //     var items = await query
       //         .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
       //         .Take(requestParameters.PageSize)
       //         .Select(p => new HomePageProductDto
       //         {
       //             ProductId = p.ProductId,
       //             OldPrice = p.OldPrice,
       //             ProductName = p.ProductName,
       //             Price = p.Price,
       //             SellerName = p.SellerProfile != null
       //                 ? p.SellerProfile.SellerName
       //                 : "Unknown",
       //             SellerProfileId = p.SellerProfileId,
       //             ProductImageId = p.Images
       //                 .OrderByDescending(i => i.IsPrimary)
       //                 .Select(i => i.ProductImageId)
       //                 .FirstOrDefault()
       //         })
       //         .ToListAsync();

       //     return PagedList<HomePageProductDto>.ToPagedList(items, requestParameters.PageNumber, requestParameters.PageSize);

       // }


        public async Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProductsByCategory(bool tracking, string categoryName)
        {
            var query = FindByCondition(p => p.Category != null && p.Category.CategoryName == categoryName, tracking); return await query
       .AsSplitQuery()
       .Select(x => new ShowProductMiniDetailsDto
       {
           ProductId = x.ProductId,
           ProductName = x.ProductName,
           Price = x.Price,
           CategoryName = x.Category != null ? x.Category.CategoryName : "General",
           ProductAttributeValues = x.ProductAttributeValues.Any() ? x.ProductAttributeValues.Select(k => new ProductAttributeValueDto()
           {
               ProductAttributeValueId = k.ProductAttributeValueId,
               ProductId = k.ProductId,
               CategoryAttributeId = k.CategoryAttributeId,
               StringValue = k.StringValue,
               IntValue = k.IntValue,
               DecimalValue = k.DecimalValue,
               BoolValue = k.BoolValue,



           }).ToList() : new List<ProductAttributeValueDto>(),

           ProductImageRefs = x.Images.Any() ? x.Images.Select(i => new ProductImageRefDto
           {
               ProductImageId = i.ProductImageId,
               IsPrimary = i.IsPrimary

           }).ToList() : new List<ProductImageRefDto>(),

           SellerProfileId = x.SellerProfileId,
           SellerName = x.SellerProfile != null ? $"{x.SellerProfile.SellerName}" : "Unkown Seller",
       })
       .ToListAsync();
        }


        public async Task<ShowProductDto?> FindProductById(bool tracking, Guid productId)
        {
            var productQ = FindByCondition(p => p.ProductId == productId, tracking);

            return await productQ
                .Select(p => new ShowProductDto
                {
                    ProductId = p.ProductId,
                    Price = p.Price,
                    OldPrice = p.OldPrice,
                    SellerUserProfileId = p.SellerProfile != null ? p.SellerProfile.SellerId : Guid.Empty,

                    ProductName = p.ProductName,
                    Category = p.Category != null ? p.Category.CategoryName : "Not Categorised",
                    StockQuantity = p.StockQuantity,
                    CreatedAt = p.CreatedAt,
                    SellerProfileId = p.SellerProfileId,
                    
                    SellerName = p.SellerProfile != null ? $"{p.SellerProfile.SellerName}" : "Unkown Seller",
                    ProductAttributeValues = p.ProductAttributeValues.Any() ? p.ProductAttributeValues.Select(k => new ProductAttributeValueDto()
                    {
                        ProductAttributeValueId = k.ProductAttributeValueId,
                        ProductId = k.ProductId,
                        CategoryAttributeId = k.CategoryAttributeId,
                        StringValue = k.StringValue,
                        IntValue = k.IntValue,
                        DecimalValue = k.DecimalValue,
                        BoolValue = k.BoolValue,



                    }).ToList() : new List<ProductAttributeValueDto>(),

                    Reviews = p.Reviews.Any() ? p.Reviews.Select(r => new ShowReviewDto
                    {
                        ReviewId = r.ReviewId,
                        ReviewerName = string.Join(" ", r.Reviewer != null ? r.Reviewer.FirstName : "Unknown", r.Reviewer != null ? r.Reviewer.LastName : "Unknown").Trim(),
                        Rating = r.Rating,
                        Comment = r.Comment,
                        CreatedAt = r.CreatedAt
                    }).ToList() : new List<ShowReviewDto>(),

                    ProductImageRefs = p.Images.Select(i => new ProductImageRefDto
                    {
                        ProductImageId = i.ProductImageId,
                        IsPrimary = i.IsPrimary,
                        IsProcessed = i.IsProcessed,
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductDataDto?> GetProductData(Guid productId)
        {
            var product = await FindByCondition(p => p.ProductId == productId, false)
     .Select(p => new
     {
         Product = new ProductDataDto
         {
             ProductId = p.ProductId,
             Price = p.Price,
             OldPrice = p.OldPrice,
             MeasurementUnit = p.MeasurementUnit,
             ProductName = p.ProductName,
             Category = p.Category != null ? p.Category.CategoryName : "Not Categorised",
             StockQuantity = p.StockQuantity,
             CreatedAt = p.CreatedAt,
             SellerProfileId = p.SellerProfileId,
             SellerName = p.SellerProfile != null ? p.SellerProfile.SellerName : "Unknown Seller",
             SellerUserProfileId = p.SellerProfile != null ? p.SellerProfile.SellerId : Guid.Empty,

             ProductAttributeDataDto = p.ProductAttributeValues.Select(k => new ProductAttributeDataDto
             {
                 ProductAttributeValueId = k.ProductAttributeValueId,
                 CategoryAttributeId = k.CategoryAttributeId,
                 AttributeDataType = k.CategoryAttribute!.AttributeDataType!.DataTypeName,
                 AttributeName = k.CategoryAttribute.AttributeName,
                 StringValue = k.StringValue,
                 IntValue = k.IntValue,
                 DecimalValue = k.DecimalValue,
                 BoolValue = k.BoolValue
             }).ToList(),

             Reviews = p.Reviews.Select(r => new ShowReviewDto
             {
                 ReviewId = r.ReviewId,
                 ReviewerName = r.Reviewer != null
                     ? r.Reviewer.FirstName + " " + r.Reviewer.LastName
                     : "Unknown",
                 Rating = r.Rating,
                 Comment = r.Comment,
                 CreatedAt = r.CreatedAt
             }).ToList(),

             ProductImageRefs = p.Images.Select(i => new ProductImageRefDto
             {
                 ProductImageId = i.ProductImageId,
                 IsPrimary = i.IsPrimary,
                 IsProcessed = i.IsProcessed
             }).ToList()
         },
         p.CategoryId
     })
     .FirstOrDefaultAsync();

            if (product == null)
                return null;

            var relatedProducts = await FindByCondition(
                    rp => rp.CategoryId == product.CategoryId && rp.ProductId != product.Product.ProductId,
                    false)
                .Select(p => new HomePageProductDto
                {
                    ProductId = p.ProductId,
                    OldPrice = p.OldPrice,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    SellerName = p.SellerProfile != null
                        ? p.SellerProfile.SellerName
                        : "Unknown",
                    SellerProfileId = p.SellerProfileId,
                    ProductImageId = p.Images
                        .OrderByDescending(i => i.IsPrimary)
                        .Select(i => i.ProductImageId)
                        .FirstOrDefault()
                })
                .Take(6) // optional limit
                .ToListAsync();

            product.Product.RelatedProducts = relatedProducts;

            return product.Product;
        }

        public async Task<double> GetProductStockQuantity(Guid productId)
        {
            return await FindByCondition(p => p.ProductId == productId, false)
                .Select(p => p.StockQuantity)
                .SingleAsync();
        }
        public Task<Product?> FindProductForUpdate(Guid productId)
        {
            return FindByCondition(p => p.ProductId == productId, true).FirstOrDefaultAsync();
        }
        public Guid CreateProduct(Product product)
        {
            CreateBase(product);
            return product.ProductId;
        }
        public void UpdateProduct(Product product) => UpdateBase(product);
        public void DeleteProduct(Product product) => DeleteBase(product);
        public async Task<ShowProductDto?> FindSellerProduct(Guid productId)
        {
            var productQ = FindByCondition(p => p.ProductId == productId, false);

            return await productQ
                .Select(p => new ShowProductDto
                {
                    ProductId = p.ProductId,
                    Price = p.Price,
                    OldPrice = p.OldPrice,
                    ProductName = p.ProductName,
                    Category = p.Category != null ? p.Category.CategoryName : "Not Categorised",
                    StockQuantity = p.StockQuantity,
                    CreatedAt = p.CreatedAt,
                    HasImage = p.HasImage,
                    SellerProfileId = p.SellerProfileId,
                    SellerName = p.SellerProfile != null ? $"{p.SellerProfile.SellerName}" : "Unkown Seller",
                    ProductAttributeValues = p.ProductAttributeValues.Any() ? p.ProductAttributeValues.Select(k => new ProductAttributeValueDto()
                    {
                        ProductAttributeValueId = k.ProductAttributeValueId,
                        ProductId = k.ProductId,
                        CategoryAttributeId = k.CategoryAttributeId,
                        StringValue = k.StringValue,
                        IntValue = k.IntValue,
                        DecimalValue = k.DecimalValue,
                        BoolValue = k.BoolValue,

                    }).ToList() : new List<ProductAttributeValueDto>(),

                    Reviews = p.Reviews.Any() ? p.Reviews.Select(r => new ShowReviewDto
                    {
                        ReviewId = r.ReviewId,
                        ReviewerName = string.Join(" ", r.Reviewer != null ? r.Reviewer.FirstName : "Unknown", r.Reviewer != null ? r.Reviewer.LastName : "Unknown").Trim(),
                        Rating = r.Rating,
                        Comment = r.Comment,
                        CreatedAt = r.CreatedAt
                    }).ToList() : new List<ShowReviewDto>(),

                    ProductImageRefs = p.Images.Select(i => new ProductImageRefDto
                    {
                        ProductImageId = i.ProductImageId,
                        IsPrimary = i.IsPrimary,
                        IsProcessed = i.IsProcessed,
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
        public async Task<ICollection<SellerProductsListDto>> GetGroupMembersProducts(IList<Guid> groupMemberIds)
        {
            if (groupMemberIds == null || groupMemberIds.Count == 0)
                return [];

            var products = await FindByCondition(p => p.SellerProfileId != Guid.Empty &&
                         groupMemberIds.Contains(p.SellerProfileId) &&
                         !p.IsDeleted &&
                         p.IsActive,
                    false)
                .Select(t => new
                {
                    SellerId = t.SellerProfileId,
                    Trade = new SellerProductDto
                    {
                        ProductId = t.ProductId,
                        ProductName = t.ProductName,
                        Price = t.Price,
                        CategoryName = t.Category != null ? t.Category.CategoryName : "Not Categorised",
                        ReviewSummary = t.Reviews.Any() ? (int)t.Reviews.Average(r => r.Rating) : 0,
                        ProductImageId = t.Images
                        .OrderByDescending(i => i.IsPrimary)
                        .Select(i => i.ProductImageId).FirstOrDefault(),


                        StockQuantity = t.StockQuantity,
                        SellerName = t.SellerProfile != null
                            ? t.SellerProfile.SellerName
                            : "Unknown Seller"
                    }
                })
                .ToListAsync();

            var result = products
                .GroupBy(t => t.SellerId)
                .Select(g => new SellerProductsListDto
                {
                    SellerProducts = g.Select(x => x.Trade).ToList()
                })
                .ToList();

            return result.Count > 0 ? result : [];
        }


        private string GenerateCacheKey(ProductRequestParameters p)
        {
            return $"products_" + $"cat_{p.CategoryId}_" + $"sub_{p.SubCategoryId}_" + $"man_{p.Manufacturer}_" + $"min_{p.MinPrice}_" + $"max_{p.MaxPrice}_" + $"name_{p.ProductName}_" + $"page_{p.PageNumber}_" + $"size_{p.PageSize}";
        }

        public async Task<PagedList<HomePageProductDto>> GetHomePageProducts(
       ProductRequestParameters requestParameters)
        {
            var cacheKey = GenerateCacheKey(requestParameters);

            if (_cache.TryGetValue(cacheKey, out PagedList<HomePageProductDto> cachedResult))
            {
                return cachedResult; // ⚡ instant return
            }

            var query = FindAll(false)
                .Where(p => p.IsActive && !p.IsDeleted &&
                       (p.SellerProfile != null ? p.SellerProfile.SellerTypeId == 1 : true))
                .AsNoTracking(); // 🔥 important for performance

            // Filters (unchanged)
            if (requestParameters.CategoryId.HasValue && requestParameters.CategoryId.Value > 0)
            {
                query = query.Where(p => p.Category != null &&
                                         p.Category.CategoryId == requestParameters.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(requestParameters.Manufacturer))
            {
                var name = requestParameters.Manufacturer.Trim();
                query = query.Where(p => p.Manufacturer.Contains(name));
            }

            if (requestParameters.SubCategoryId.HasValue && requestParameters.SubCategoryId.Value > 0)
            {
                query = query.Where(p => p.SubCategory != null &&
                                         p.SubCategory.SubCategoryId == requestParameters.SubCategoryId);
            }

            if (requestParameters.MinPrice.HasValue)
                query = query.Where(p => p.Price >= requestParameters.MinPrice.Value);

            if (requestParameters.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= requestParameters.MaxPrice.Value);

            if (!string.IsNullOrWhiteSpace(requestParameters.ProductName))
            {
                var name = requestParameters.ProductName.Trim().ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(name));
            }

            query = query.OrderByDescending(p => p.CreatedAt);

            var count = await query.CountAsync();

            var items = await query
                .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
                .Take(requestParameters.PageSize)
                .Select(p => new HomePageProductDto
                {
                    ProductId = p.ProductId,
                    OldPrice = p.OldPrice,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    SellerName = p.SellerProfile != null
                        ? p.SellerProfile.SellerName
                        : "Unknown",
                    SellerProfileId = p.SellerProfileId,
                    ProductImageId = p.Images
                        .OrderByDescending(i => i.IsPrimary)
                        .Select(i => i.ProductImageId)
                        .FirstOrDefault()
                })
                .ToListAsync();

            var result = PagedList<HomePageProductDto>.ToPagedList(items, requestParameters.PageNumber, requestParameters.PageSize);

            // 🧠 Cache options
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(2))   // short = fresh data
                .SetSlidingExpiration(TimeSpan.FromMinutes(1));

            _cache.Set(cacheKey, result, cacheOptions);

            return result;
        }

    }
}


