using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Repository.context;
using Shared.Dtos;
using Shared.RequestFeatures;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace Repository.Repos;

public sealed class ProductRepo : RepositoryBase<Product>, IProductRepo
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _cache;

    private CancellationTokenSource _productCacheReset = new();

    private const string UnknownSeller = "Unknown Seller";
    private const string DefaultCategory = "Not Categorised";

    public ProductRepo(
        ApplicationDbContext context,
        IMemoryCache cache) : base(context)
    {
        _context = context;
        _cache = cache;
    }

    #region Base Query

    private IQueryable<Product> BaseQuery(bool tracking = false)
    {
        var query = tracking
            ? FindAll(true)
            : FindAll(false).AsNoTracking();

        return query.Where(p => !p.IsDeleted);
    }

    #endregion

    #region Projections

    private static readonly Expression<Func<Product, ShowProductMiniDetailsDto>>
        MiniProductProjection = p => new ShowProductMiniDetailsDto
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Price = p.Price,
            Condition = p.Condition,
            SlugName = p.Slug,

            CategoryName = p.Category != null
                ? p.Category.CategoryName
                : DefaultCategory,

            SellerProfileId = p.SellerProfileId,

            SellerName = p.SellerProfile != null
                ? p.SellerProfile.SellerName
                : UnknownSeller,

            SellerSlugName = p.SellerProfile != null
                ? p.SellerProfile.Slug
                : UnknownSeller,

            ProductImageRefs = p.Images
                .Select(i => new ProductImageRefDto
                {
                    ProductImageId = i.ProductImageId,
                    IsPrimary = i.IsPrimary
                })
                .ToList()
        };

    private static readonly Expression<Func<Product, ShowProductDto>>
        ShowProductProjection = p => new ShowProductDto
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Price = p.Price,
            OldPrice = p.OldPrice,
            Condition = p.Condition,
            SlugName = p.Slug,

            SellerProfileId = p.SellerProfileId,

            SellerName = p.SellerProfile != null
                ? p.SellerProfile.SellerName
                : UnknownSeller,

            SellerSlugName = p.SellerProfile != null
                ? p.SellerProfile.Slug
                : UnknownSeller,

            SellerUserProfileId = p.SellerProfile != null
                ? p.SellerProfile.SellerId
                : Guid.Empty,

            Category = p.Category != null
                ? p.Category.CategoryName
                : DefaultCategory,

            CreatedAt = p.CreatedAt,

            HasImage = p.HasImage,

            Reviews = p.ProductReviews
                .Select(r => new ShowReviewDto
                {
                    ReviewId = r.ProductReviewId,

                    ReviewerName =
                        r.Reviewer != null &&
                        r.Reviewer.IdentityUser != null
                            ? (
                                (r.Reviewer.IdentityUser.FirstName ?? "") +
                                " " +
                                (r.Reviewer.IdentityUser.LastName ?? "")
                              ).Trim()
                            : "Unknown",

                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                })
                .ToList(),

            ProductImageRefs = p.Images
                .Select(i => new ProductImageRefDto
                {
                    ProductImageId = i.ProductImageId,
                    IsPrimary = i.IsPrimary,
                    IsProcessed = i.IsProcessed
                })
                .ToList()
        };

    private static readonly Expression<Func<Product, HomePageProductDto>>
        HomeProductProjection = p => new HomePageProductDto
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Price = p.Price,
            OldPrice = p.OldPrice,
            SlugName = p.Slug,

            SellerProfileId = p.SellerProfileId,

            SellerName = p.SellerProfile != null
                ? p.SellerProfile.SellerName
                : UnknownSeller,

            SellerSlugName = p.SellerProfile != null
                ? p.SellerProfile.Slug
                : UnknownSeller,

            ProductImageId = p.Images
                .OrderByDescending(i => i.IsPrimary)
                .Select(i => i.ProductImageId)
                .FirstOrDefault()
        };

    private static readonly Expression<Func<Product, SellerProductDto>>
        SellerProductProjection = p => new SellerProductDto
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Price = p.Price,
            Condition = p.Condition,
            SlugName = p.Slug,

            SellerName = p.SellerProfile != null
                ? p.SellerProfile.SellerName
                : UnknownSeller,

            SellerSlugName = p.SellerProfile != null
                ? p.SellerProfile.Slug
                : UnknownSeller,

            CategoryName = p.Category != null
                ? p.Category.CategoryName
                : DefaultCategory,

            ReviewSummary = p.ProductReviews.Any()
                ? (int)p.ProductReviews.Average(r => r.Rating)
                : 0,

            ProductImageId = p.Images
                .OrderByDescending(i => i.IsPrimary)
                .Select(i => i.ProductImageId)
                .FirstOrDefault()
        };

    #endregion

    #region Cache

    private async Task<T?> GetOrCreateCacheAsync<T>(
        string key,
        Func<Task<T?>> factory,
        int minutes = 20)
    {
        return await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow =
                TimeSpan.FromMinutes(minutes);

            entry.AddExpirationToken(
                new CancellationChangeToken(
                    _productCacheReset.Token));

            return await factory();
        });
    }

    private void ResetProductCache()
    {
        var old = Interlocked.Exchange(
            ref _productCacheReset,
            new CancellationTokenSource());

        old.Cancel();
        old.Dispose();
    }

    private static class CacheKeys
    {
        public static string Product(Guid id)
            => $"product:{id}";

        public static string ProductSlug(string slug)
            => $"product:slug:{slug}";

        public static string GroupProducts(Guid groupId)
            => $"group-products:{groupId}";

        public static string GroupMembers(string hash)
            => $"group-members:{hash}";

        public static string HomePage(ProductRequestParameters p)
            => $"homepage:{p.CategoryId}:{p.SubCategoryId}:{p.PageNumber}:{p.PageSize}:{p.ProductName}:{p.MinPrice}:{p.MaxPrice}";

        public const string HomePageCustom = "homepage-custom";
    }

    #endregion

    #region CRUD

    public Guid CreateProduct(Product product)
    {
        CreateBase(product);

        ResetProductCache();

        return product.ProductId;
    }

    public void UpdateProduct(Product product)
    {
        UpdateBase(product);

        ResetProductCache();
    }

    public void DeleteProduct(Product product)
    {
        DeleteBase(product);

        ResetProductCache();
    }

    #endregion

    #region Basic Queries

    public Task<int> NumberOfProducts()
        => BaseQuery().CountAsync();

    public Task<Guid> GetProductIdBySlugName(string slug)
        => BaseQuery()
            .Where(p => p.Slug == slug)
            .Select(p => p.ProductId)
            .FirstOrDefaultAsync();

    public Task<Product?> FindProductForUpdate(Guid productId)
        => BaseQuery(true)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

    public Task MakeAllProductsFeatured()
        => BaseQuery(true)
            .Where(p => !p.IsFeatured)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(p => p.IsFeatured, true));

    #endregion

    #region Product Lists

    public async Task<IEnumerable<ShowProductMiniDetailsDto>>
        GetAllProducts(bool tracking)
    {
        return await BaseQuery(tracking)
            .Select(MiniProductProjection)
            .ToListAsync();
    }

    public async Task<IEnumerable<ShowProductMiniDetailsDto>>
        GetAllProductsByCategory(
            bool tracking,
            string categoryName)
    {
        return await BaseQuery(tracking)
            .Where(p =>
                p.Category != null &&
                p.Category.CategoryName == categoryName)
            .Select(MiniProductProjection)
            .ToListAsync();
    }

    #endregion

    #region Product Details

    public Task<ShowProductDto?> FindProductById(
        bool tracking,
        Guid productId)
    {
        return BaseQuery(tracking)
            .Where(p => p.ProductId == productId)
            .Select(ShowProductProjection)
            .FirstOrDefaultAsync();
    }

    public Task<ShowProductDto?> FindProductBySlugName(
        bool tracking,
        string slug)
    {
        return BaseQuery(tracking)
            .Where(p => p.Slug == slug)
            .Select(ShowProductProjection)
            .FirstOrDefaultAsync();
    }

    public Task<ShowProductDto?> FindSellerProduct(Guid productId)
        => FindProductById(false, productId);

    public Task<ShowProductDto?> FindSellerProductUsingSlugName(
        bool tracking,
        string slug)
        => FindProductBySlugName(tracking, slug);

    #endregion

    #region Product Data

    public async Task<ProductDataDto?> GetProductData(Guid productId)
    {
        var cacheKey = CacheKeys.Product(productId);

        return await GetOrCreateCacheAsync(
            cacheKey,
            async () =>
            {
                var product = await BaseQuery()
                    .Where(p => p.ProductId == productId)
                    .Select(p => new
                    {
                        Product = new ProductDataDto
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            Price = p.Price,
                            OldPrice = p.OldPrice,
                            Condition = p.Condition,
                            SlugName = p.Slug,

                            SellerProfileId = p.SellerProfileId,

                            SellerName = p.SellerProfile != null
                                ? p.SellerProfile.SellerName
                                : UnknownSeller,

                            SellerSlugName = p.SellerProfile != null
                                ? p.SellerProfile.Slug
                                : UnknownSeller,

                            SellerUserProfileId =
                                p.SellerProfile != null
                                    ? p.SellerProfile.SellerId
                                    : Guid.Empty,

                            Category = p.Category != null
                                ? p.Category.CategoryName
                                : DefaultCategory,

                            CreatedAt = p.CreatedAt,

                            Impressions =
                                p.ProductImpressions.Count(),

                            Reviews = p.ProductReviews
                                .Select(r => new ShowReviewDto
                                {
                                    ReviewId = r.ProductReviewId,

                                    ReviewerName =
                                        r.Reviewer != null &&
                                        r.Reviewer.IdentityUser != null
                                            ? (
                                                (r.Reviewer.IdentityUser.FirstName ?? "") +
                                                " " +
                                                (r.Reviewer.IdentityUser.LastName ?? "")
                                              ).Trim()
                                            : "Unknown",

                                    Rating = r.Rating,
                                    Comment = r.Comment,
                                    CreatedAt = r.CreatedAt
                                })
                                .ToList(),

                            ProductImageRefs = p.Images
                                .Select(i => new ProductImageRefDto
                                {
                                    ProductImageId =
                                        i.ProductImageId,

                                    IsPrimary = i.IsPrimary,
                                    IsProcessed = i.IsProcessed
                                })
                                .ToList()
                        },

                        p.CategoryId
                    })
                    .FirstOrDefaultAsync();

                if (product == null)
                    return null;

                product.Product.RelatedProducts =
                    await BaseQuery()
                        .Where(r =>
                            r.CategoryId == product.CategoryId &&
                            r.ProductId != product.Product.ProductId)
                        .OrderByDescending(r => r.CreatedAt)
                        .Take(12)
                        .Select(HomeProductProjection)
                        .ToListAsync();

                return product.Product;
            });
    }

    public async Task<ProductDataDto?> GetProductDataUsingSlugName(
        string slug)
    {
        var productId = await GetProductIdBySlugName(slug);

        if (productId == Guid.Empty)
            return null;

        return await GetProductData(productId);
    }

    #endregion

    #region Homepage Products

    public async Task<PagedList<HomePageProductDto>>
        GetHomePageProducts(
            ProductRequestParameters request)
    {
        var cacheKey = CacheKeys.HomePage(request);

        var cached =
            await GetOrCreateCacheAsync(cacheKey,async () =>  {
                    var query = BaseQuery().Where(p =>
                            p.IsActive &&
                            (
                                p.SellerProfileId!=Guid.Empty
                            ));

                    if (request.CategoryId>0)
                    {
                        query = query.Where(p =>
                            p.CategoryId ==
                            request.CategoryId.Value);
                    }

                    if (request.SubCategoryId > 0)
                    {
                        query = query.Where(p =>
                            p.SubCategoryId ==
                            request.SubCategoryId.Value);
                    }

                    if (request.MinPrice > 0)
                    {
                        query = query.Where(p =>
                            p.Price >= request.MinPrice.Value);
                    }

                    if (request.MaxPrice.HasValue)
                    {
                        query = query.Where(p =>
                            p.Price <= request.MaxPrice.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(
                            request.ProductName))
                    {
                        var search =
                            request.ProductName.Trim();

                        query = query.Where(p =>
                            EF.Functions.Like(
                                p.ProductName,
                                $"%{search}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(
                            request.ProductDescription))
                    {
                        var desc =
                            request.ProductDescription.Trim();

                        query = query.Where(p =>
                            EF.Functions.Like(
                                p.Description,
                                $"%{desc}%"));
                    }

                    query = query
                        .OrderByDescending(p => p.CreatedAt);

                    var count = await query.CountAsync();

                    var items = await query
                        .Skip(
                            (request.PageNumber - 1) *
                            request.PageSize)
                        .Take(request.PageSize)
                        .Select(HomeProductProjection)
                        .ToListAsync();

                    return PagedList<HomePageProductDto>
                        .ToPagedList(
                            items,
                            request.PageNumber,
                            request.PageSize);
                });

        return cached!;
    }

    #endregion

    #region Homepage Custom

    public async Task<HomePageCustomProductsDto?>
        HomePageCustomProducts()
    {
        return await GetOrCreateCacheAsync(
            CacheKeys.HomePageCustom,
            async () =>
            {
                var baseQuery = BaseQuery()
                    .Where(p =>
                        p.IsActive &&
                        (
                            p.SellerProfile == null ||
                            p.SellerProfile.SellerTypeId == 1
                        ));

                var featuredProducts = await baseQuery
                    .Where(p => p.IsFeatured)
                    .OrderByDescending(p => p.CreatedAt)
                    .Take(20)
                    .Select(HomeProductProjection)
                    .ToListAsync();

                var topGroupId = await _context.GroupMembers
                    .AsNoTracking()
                    .GroupBy(g => g.UserGroupId)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefaultAsync();

                var groupProducts =
                    new List<HomePageProductDto>();

                if (topGroupId != Guid.Empty)
                {
                    var sellerIds =
                        await _context.GroupMembers
                            .AsNoTracking()
                            .Where(g =>
                                g.UserGroupId == topGroupId)
                            .Select(g => g.UserProfileId)
                            .ToListAsync();

                    groupProducts = await baseQuery
                        .Where(p =>
                            sellerIds.Contains(
                                p.SellerProfileId))
                        .OrderByDescending(p => p.CreatedAt)
                        .Take(20)
                        .Select(HomeProductProjection)
                        .ToListAsync();
                }

                var cards = await _context.HomePageCards
                    .AsNoTracking()
                    .Include(c => c.CategoryLinks)
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.Index)
                    .ToListAsync();

                var allCategoryIds = cards
                    .SelectMany(c =>
                        c.CategoryLinks
                            .Select(cl => cl.CategoryId))
                    .Distinct()
                    .ToList();

                var allProducts = await baseQuery
                    .Where(p =>
                        allCategoryIds.Contains(
                            p.CategoryId))
                    .Select(p => new
                    {
                        p.CategoryId,
                        Product = new HomePageProductDto
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            Price = p.Price,
                            OldPrice = p.OldPrice,
                            SlugName = p.Slug,

                            SellerProfileId =
                                p.SellerProfileId,

                            SellerName =
                                p.SellerProfile != null
                                    ? p.SellerProfile.SellerName
                                    : UnknownSeller,

                            SellerSlugName =
                                p.SellerProfile != null
                                    ? p.SellerProfile.Slug
                                    : UnknownSeller,

                            ProductImageId = p.Images
                                .OrderByDescending(i =>
                                    i.IsPrimary)
                                .Select(i =>
                                    i.ProductImageId)
                                .FirstOrDefault()
                        }
                    })
                    .ToListAsync();

                var groupedProducts = allProducts
                    .GroupBy(x => x.CategoryId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.Product)
                              .ToList());

                var advertisedProducts =
                    new List<HomeProductCardDto>();

                foreach (var card in cards)
                {
                    var categoryIds = card.CategoryLinks
                        .Select(cl => cl.CategoryId);

                    var take = card.IsRow ? 15 : 4;

                    var products = categoryIds
                        .Where(groupedProducts.ContainsKey)
                        .SelectMany(id =>
                            groupedProducts[id])
                        .Take(take)
                        .ToList();

                    advertisedProducts.Add(
                        new HomeProductCardDto
                        {
                            HomePageCardId =
                                card.HomePageCardId,

                            Title = card.Title,
                            Color = card.Color,
                            LinkLabel = card.LinkLabel,
                            Index = card.Index,
                            IsRow = card.IsRow,
                            Products = products
                        });
                }

                return new HomePageCustomProductsDto
                {
                    FeaturedProducts = featuredProducts,
                    GroupProducts = groupProducts,
                    AdvertisedProducts = advertisedProducts
                };
            },
            30);
    }

    #endregion

    #region Group Products

    public async Task<ICollection<SellerProductsListDto>>
        GetGroupMembersProducts(
            IList<Guid> groupMemberIds)
    {
        if (groupMemberIds == null ||
            groupMemberIds.Count == 0)
        {
            return [];
        }

        var hash = ComputeHash(groupMemberIds);

        var cacheKey =
            CacheKeys.GroupMembers(hash);

        return await GetOrCreateCacheAsync(
            cacheKey,
            async () =>
            {
                var sellerIds =
                    groupMemberIds.ToHashSet();

                var products = await BaseQuery()
                    .Where(p =>
                        p.IsActive &&
                        sellerIds.Contains(
                            p.SellerProfileId))
                    .Select(p => new
                    {
                        p.SellerProfileId,
                        Product =
                            new SellerProductDto
                            {
                                ProductId = p.ProductId,
                                ProductName =
                                    p.ProductName,

                                Price = p.Price,
                                Condition =
                                    p.Condition,

                                SlugName = p.Slug,

                                SellerName =
                                    p.SellerProfile != null
                                        ? p.SellerProfile.SellerName
                                        : UnknownSeller,

                                SellerSlugName =
                                    p.SellerProfile != null
                                        ? p.SellerProfile.Slug
                                        : UnknownSeller,

                                CategoryName =
                                    p.Category != null
                                        ? p.Category.CategoryName
                                        : DefaultCategory,

                                ReviewSummary =
                                    p.ProductReviews.Any()
                                        ? (int)p.ProductReviews
                                            .Average(r =>
                                                r.Rating)
                                        : 0,

                                ProductImageId =
                                    p.Images
                                        .OrderByDescending(i =>
                                            i.IsPrimary)
                                        .Select(i =>
                                            i.ProductImageId)
                                        .FirstOrDefault()
                            }
                    })
                    .ToListAsync();

                return products
                    .GroupBy(x => x.SellerProfileId)
                    .Select(g =>
                        new SellerProductsListDto
                        {
                            SellerProducts =
                                g.Select(x => x.Product)
                                    .ToList()
                        })
                    .ToList();
            }) ?? [];
    }

    public async Task<ICollection<SellerProductsListDto>>
        GetGroupMembersForDisplayProducts(
            IList<Guid> groupMemberIds,
            Guid groupId)
    {
        if (groupMemberIds == null ||
            groupMemberIds.Count == 0)
        {
            return [];
        }

        const int MaxProducts = 50;

        return await GetOrCreateCacheAsync(
            CacheKeys.GroupProducts(groupId),
            async () =>
            {
                var featuredIds =
                    await _context.GroupFeaturedProducts
                        .AsNoTracking()
                        .Where(x =>
                            x.UserGroupId == groupId)
                        .Select(x => x.ProductId)
                        .Take(MaxProducts)
                        .ToListAsync();

                var sellerIds =
                    groupMemberIds.ToHashSet();

                var products = await BaseQuery()
                    .Where(p =>
                        p.IsActive &&
                        sellerIds.Contains(
                            p.SellerProfileId))
                    .OrderByDescending(p =>
                        featuredIds.Contains(
                            p.ProductId))
                    .ThenByDescending(p =>
                        p.CreatedAt)
                    .Take(MaxProducts)
                    .Select(p => new
                    {
                        p.SellerProfileId,
                        Product =
                            new SellerProductDto
                            {
                                ProductId = p.ProductId,
                                ProductName =
                                    p.ProductName,

                                Price = p.Price,
                                Condition =
                                    p.Condition,

                                SlugName = p.Slug,

                                SellerName =
                                    p.SellerProfile != null
                                        ? p.SellerProfile.SellerName
                                        : UnknownSeller,

                                SellerSlugName =
                                    p.SellerProfile != null
                                        ? p.SellerProfile.Slug
                                        : UnknownSeller,

                                CategoryName =
                                    p.Category != null
                                        ? p.Category.CategoryName
                                        : DefaultCategory,

                                ReviewSummary =
                                    p.ProductReviews.Any()
                                        ? (int)p.ProductReviews
                                            .Average(r =>
                                                r.Rating)
                                        : 0,

                                ProductImageId =
                                    p.Images
                                        .OrderByDescending(i =>
                                            i.IsPrimary)
                                        .Select(i =>
                                            i.ProductImageId)
                                        .FirstOrDefault()
                            }
                    })
                    .ToListAsync();

                return products
                    .GroupBy(x => x.SellerProfileId)
                    .Select(g =>
                        new SellerProductsListDto
                        {
                            SellerProducts =
                                g.Select(x => x.Product)
                                    .ToList()
                        })
                    .ToList();
            },
            30) ?? [];
    }

    #endregion

    #region Helpers

    private static string ComputeHash(
        IList<Guid> ids)
    {
        var ordered = ids.OrderBy(x => x);

        using var md5 = MD5.Create();

        var bytes = ordered
            .SelectMany(x => x.ToByteArray())
            .ToArray();

        return Convert.ToHexString(
            md5.ComputeHash(bytes));
    }

    #endregion
}