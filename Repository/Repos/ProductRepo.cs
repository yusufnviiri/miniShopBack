using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Repository.context;
using Shared.Dtos;
using Shared.RequestFeatures;
using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading;

namespace Repository.Repos;

public sealed class ProductRepo : RepositoryBase<Product>, IProductRepo
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _cache;

    private static CancellationTokenSource _productCacheReset = new();
    private const string UnknownSeller = "Unknown Seller";
    private const string DefaultCategory = "Not Categorised";

    public ProductRepo(ApplicationDbContext context, IMemoryCache cache)
        : base(context)
    {
        _context = context;
        _cache = cache;
    }

    // ============================================================
    //  Base query
    // ============================================================

    private IQueryable<Product> BaseQuery(bool tracking = false)
    {
        var query = tracking ? FindAll(true) : FindAll(false).AsNoTracking();
        return query.Where(p => !p.IsDeleted);
    }

    // ============================================================
    //  Projections
    // ============================================================

    private static readonly Expression<Func<Product, ShowProductMiniDetailsDto>>
        MiniProductProjection = p => new ShowProductMiniDetailsDto
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Price = p.Price,
            Condition = p.Condition,
            SlugName = p.Slug,
            CategoryName = p.Category != null ? p.Category.CategoryName : DefaultCategory,
            SellerProfileId = p.SellerProfileId,
            SellerName = p.SellerProfile != null ? p.SellerProfile.SellerName : UnknownSeller,
            SellerSlugName = p.SellerProfile != null ? p.SellerProfile.Slug : UnknownSeller,
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
            SellerName = p.SellerProfile != null ? p.SellerProfile.SellerName : UnknownSeller,
            SellerSlugName = p.SellerProfile != null ? p.SellerProfile.Slug : UnknownSeller,
            SellerUserProfileId = p.SellerProfile != null ? p.SellerProfile.SellerId : Guid.Empty,
            Category = p.Category != null ? p.Category.CategoryName : DefaultCategory,
            CreatedAt = p.CreatedAt,
            HasImage = p.HasImage,
            Reviews = p.ProductReviews
                .Select(r => new ShowReviewDto
                {
                    ReviewId = r.ProductReviewId,
                    ReviewerName = r.Reviewer != null && r.Reviewer.IdentityUser != null
                        ? ((r.Reviewer.IdentityUser.FirstName ?? "") + " " +
                           (r.Reviewer.IdentityUser.LastName ?? "")).Trim()
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
            SellerName = p.SellerProfile != null ? p.SellerProfile.SellerName : UnknownSeller,
            SellerSlugName = p.SellerProfile != null ? p.SellerProfile.Slug : UnknownSeller,
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
            SellerName = p.SellerProfile != null ? p.SellerProfile.SellerName : UnknownSeller,
            SellerSlugName = p.SellerProfile != null ? p.SellerProfile.Slug : UnknownSeller,
            CategoryName = p.Category != null ? p.Category.CategoryName : DefaultCategory,
            ReviewSummary = p.ProductReviews.Any()
                ? (int)p.ProductReviews.Average(r => r.Rating)
                : 0,
            ProductImageId = p.Images
                .OrderByDescending(i => i.IsPrimary)
                .Select(i => i.ProductImageId)
                .FirstOrDefault()
        };

    // ============================================================
    //  Cache
    // ============================================================

    private async Task<T?> GetOrCreateCacheAsync<T>(
        string key,
        Func<Task<T?>> factory,
        int minutes = 20)
    {
        return await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes);
            entry.AddExpirationToken(
                new CancellationChangeToken(_productCacheReset.Token));
            return await factory();
        });
    }

    private void ResetProductCache()
    {
        var old = Interlocked.Exchange(ref _productCacheReset, new CancellationTokenSource());
        old.Cancel();
        old.Dispose();
    }

    /// <summary>
    /// Surgically invalidate only the homepage-custom payload.
    /// Call this from HomePageCard create/update/delete paths so
    /// new cards appear immediately without flushing every other
    /// product cache entry.
    /// </summary>
    public void InvalidateHomePageCards()
    {
        _cache.Remove(CacheKeys.HomePageCustom);
    }

    private static class CacheKeys
    {
        public static string Product(Guid id) => $"product:{id}";
        public static string ProductSlug(string slug) => $"product:slug:{slug}";
        public static string GroupProducts(Guid groupId) => $"group-products:{groupId}";
        public static string GroupMembers(string hash) => $"group-members:{hash}";

        public static string HomePage(ProductRequestParameters p) =>
            $"homepage:{p.CategoryId}:{p.SubCategoryId}:{p.PageNumber}:" +
            $"{p.PageSize}:{p.ProductName}:{p.MinPrice}:{p.MaxPrice}";

        public const string HomePageCustom = "homepage-custom";
        public static string LatestFeatured(int pageNumber, int pageSize) =>
    $"latest-featured:{pageNumber}:{pageSize}";
    }

    // ============================================================
    //  CRUD
    // ============================================================

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

    // ============================================================
    //  Basic queries
    // ============================================================

    public Task<int> NumberOfProducts() => BaseQuery().CountAsync();

    //public async Task<long> NextProductSlugNumberAsync() =>
    //await _context.Database.SqlQuery<long>(
    //    $"SELECT NEXT VALUE FOR ProductSlugSeq").SingleAsync();

    public async Task<long> NextProductSlugNumberAsync()
    {
        var connection = _context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        await using var command = connection.CreateCommand();

        command.CommandText = "SELECT NEXT VALUE FOR ProductSlugSeq";

        var result = await command.ExecuteScalarAsync();

        return Convert.ToInt64(result);
    }


    public Task<Guid> GetProductIdBySlugName(string slug) =>
        BaseQuery()
            .Where(p => p.Slug == slug)
            .Select(p => p.ProductId)
            .FirstOrDefaultAsync();

    public Task<Product?> FindProductForUpdate(Guid productId) =>
        BaseQuery(true).FirstOrDefaultAsync(p => p.ProductId == productId);

    public Task MakeAllProductsFeatured() =>
        BaseQuery(true)
            .Where(p => !p.IsFeatured)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsFeatured, true));

    // ============================================================
    //  Product lists
    // ============================================================

    public async Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProducts(bool tracking) =>
        await BaseQuery(tracking).Select(MiniProductProjection).ToListAsync();

    public async Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProductsByCategory(
        bool tracking, string categoryName) =>
        await BaseQuery(tracking)
            .Where(p => p.Category != null && p.Category.CategoryName == categoryName)
            .Select(MiniProductProjection)
            .ToListAsync();

    // ============================================================
    //  Product details
    // ============================================================

    public Task<ShowProductDto?> FindProductById(bool tracking, Guid productId) =>
        BaseQuery(tracking)
            .Where(p => p.ProductId == productId)
            .Select(ShowProductProjection)
            .FirstOrDefaultAsync();

    public Task<ShowProductDto?> FindProductBySlugName(bool tracking, string slug) =>
        BaseQuery(tracking)
            .Where(p => p.Slug == slug)
            .Select(ShowProductProjection)
            .FirstOrDefaultAsync();

    public Task<ShowProductDto?> FindSellerProduct(Guid productId) =>
        FindProductById(false, productId);

    public Task<ShowProductDto?> FindSellerProductUsingSlugName(bool tracking, string slug) =>
        FindProductBySlugName(tracking, slug);

    // ============================================================
    //  Product data (+ related)
    // ============================================================

    public async Task<ProductDataDto?> GetProductData(Guid productId)
    {
        return await GetOrCreateCacheAsync(CacheKeys.Product(productId), async () =>
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
                            ? p.SellerProfile.SellerName : UnknownSeller,
                        SellerSlugName = p.SellerProfile != null
                            ? p.SellerProfile.Slug : UnknownSeller,
                        SellerUserProfileId = p.SellerProfile != null
                            ? p.SellerProfile.SellerId : Guid.Empty,
                        Category = p.Category != null
                            ? p.Category.CategoryName : DefaultCategory,
                        CreatedAt = p.CreatedAt,
                        Impressions = p.ProductImpressions.Count(),
                        Reviews = p.ProductReviews
                            .Select(r => new ShowReviewDto
                            {
                                ReviewId = r.ProductReviewId,
                                ReviewerName = r.Reviewer != null && r.Reviewer.IdentityUser != null
                                    ? ((r.Reviewer.IdentityUser.FirstName ?? "") + " " +
                                       (r.Reviewer.IdentityUser.LastName ?? "")).Trim()
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
                    },
                    p.CategoryId
                })
                .FirstOrDefaultAsync();

            if (product == null) return null;

            product.Product.RelatedProducts = await BaseQuery()
                .Where(r => r.CategoryId == product.CategoryId &&
                            r.ProductId != product.Product.ProductId)
                .OrderByDescending(r => r.CreatedAt)
                .Take(12)
                .Select(HomeProductProjection)
                .ToListAsync();

            return product.Product;
        });
    }

    public async Task<ProductDataDto?> GetProductDataUsingSlugName(string slug)
    {
        var productId = await GetProductIdBySlugName(slug);
        return productId == Guid.Empty ? null : await GetProductData(productId);
    }

    // ============================================================
    //  Paged homepage products (filters)
    // ============================================================

    public async Task<PagedList<HomePageProductDto>> GetHomePageProducts(
        ProductRequestParameters request, CancellationToken ct = default)
    {
        var cached = await GetOrCreateCacheAsync(CacheKeys.HomePage(request), async () =>
        {
            var query = BaseQuery()
                .Where(p => p.IsActive && p.SellerProfileId != Guid.Empty);

            if (request.CategoryId > 0)
                query = query.Where(p => p.CategoryId == request.CategoryId.Value);

            if (request.SubCategoryId > 0)
                query = query.Where(p => p.SubCategoryId == request.SubCategoryId.Value);

            if (request.MinPrice > 0)
                query = query.Where(p => p.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= request.MaxPrice.Value);

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                var search = request.ProductName.Trim();
                query = query.Where(p => EF.Functions.Like(p.ProductName, $"%{search}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.ProductDescription))
            {
                var desc = request.ProductDescription.Trim();
                query = query.Where(p => EF.Functions.Like(p.Description, $"%{desc}%"));
            }

            query = query.OrderByDescending(p => p.CreatedAt);

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(HomeProductProjection)
                .ToListAsync(ct);

            return PagedList<HomePageProductDto>.ToPagedList(
                items, request.PageNumber, request.PageSize);
        });

        return cached!;
    }


    public async Task<PagedList<HomePageProductDto>> GetOtherProducts(
       ProductRequestParameters request)
    {
        var cached = await GetOrCreateCacheAsync(
            CacheKeys.LatestFeatured(request.PageNumber, request.PageSize),
            async () =>
            {
                var items = await BaseQuery()
                    .Where(p => p.IsActive
                             && p.SellerProfileId != Guid.Empty)
                    .OrderByDescending(p => p.IsFeatured)       // featured first (true > false)
                    .ThenByDescending(p => p.CreatedAt)         // then newest within each group
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(HomeProductProjection)
                    .ToListAsync();

                return PagedList<HomePageProductDto>.ToPagedList(
                    items, request.PageNumber, request.PageSize);
            },
            minutes: 5);

        return cached!;
    }

    // ============================================================
    //  Homepage custom (featured + top-group + cards + groups)
    // ============================================================

    public async Task<HomePageCustomProductsDto?> HomePageCustomProducts()
    {
        return await GetOrCreateCacheAsync(CacheKeys.HomePageCustom, async () =>
        {
            var baseQuery = BaseQuery()
                .Where(p =>
                    p.IsActive &&
                    (p.SellerProfile == null || p.SellerProfile.SellerTypeId == 1));

            // -------- Featured products --------
            var featuredProducts = await baseQuery
                .Where(p => p.IsFeatured)
                .OrderByDescending(p => p.CreatedAt)
                .Take(20)
                .Select(HomeProductProjection)
                .ToListAsync();

            // -------- Top-group products --------
            var groupProducts = await GetTopGroupProducts(baseQuery);

            // -------- Cards: split by kind once --------
            var cards = await _context.HomePageCards
                .AsNoTracking()
                .Include(c => c.CategoryLinks)
                .Where(c => c.IsActive)
                .OrderBy(c => c.Index)
                .ToListAsync();

            var categoryCards = cards.Where(c => !c.IsGroupCard).ToList();
            var groupCards = cards
                .Where(c => c.IsGroupCard && c.UserGroupId != Guid.Empty)
                .ToList();

            var advertisedProducts = await BuildCategoryCards(categoryCards, baseQuery);
            var selectedGroups = await BuildGroupCards(groupCards, baseQuery);

            return new HomePageCustomProductsDto
            {
                FeaturedProducts = featuredProducts,
                GroupProducts = groupProducts,
                AdvertisedProducts = advertisedProducts,
                SelectedGroups = selectedGroups
            };
        }, 30);
    }

    // ---------- Top-group products helper ----------

    private async Task<List<HomePageProductDto>> GetTopGroupProducts(
        IQueryable<Product> baseQuery)
    {
        var topGroupId = await _context.GroupMembers
            .AsNoTracking()
            .GroupBy(g => g.UserGroupId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefaultAsync();

        if (topGroupId == Guid.Empty) return new List<HomePageProductDto>();

        var memberUserProfileIds = await _context.GroupMembers
            .AsNoTracking()
            .Where(g => g.UserGroupId == topGroupId)
            .Select(g => g.UserProfileId)
            .ToListAsync();

        if (memberUserProfileIds.Count == 0) return new List<HomePageProductDto>();

        // NOTE: original code matched UserProfileId directly against
        // Product.SellerProfileId. That conflates two different keys.
        // Going through SellerProfile.SellerId is the correct chain.
        var sellerProfileIds = await _context.SellerProfiles
            .AsNoTracking()
            .Where(s => memberUserProfileIds.Contains(s.SellerId))
            .Select(s => s.SellerProfileId)
            .ToListAsync();

        if (sellerProfileIds.Count == 0) return new List<HomePageProductDto>();

        return await baseQuery
            .Where(p => sellerProfileIds.Contains(p.SellerProfileId))
            .OrderByDescending(p => p.CreatedAt)
            .Take(20)
            .Select(HomeProductProjection)
            .ToListAsync();
    }

    // ---------- Category cards helper ----------

    private async Task<List<HomeProductCardDto>> BuildCategoryCards(
        List<HomePageCard> cards,
        IQueryable<Product> baseQuery)
    {
        var result = new List<HomeProductCardDto>();
        if (cards.Count == 0) return result;

        var allCategoryIds = cards
            .SelectMany(c => c.CategoryLinks.Select(cl => cl.CategoryId))
            .Distinct()
            .ToList();

        var rows = await baseQuery
            .Where(p => allCategoryIds.Contains(p.CategoryId))
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
                    SellerProfileId = p.SellerProfileId,
                    SellerName = p.SellerProfile != null
                        ? p.SellerProfile.SellerName : UnknownSeller,
                    SellerSlugName = p.SellerProfile != null
                        ? p.SellerProfile.Slug : UnknownSeller,
                    ProductImageId = p.Images
                        .OrderByDescending(i => i.IsPrimary)
                        .Select(i => i.ProductImageId)
                        .FirstOrDefault()
                }
            })
            .ToListAsync();

        var byCategory = rows
            .GroupBy(x => x.CategoryId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Product).ToList());

        foreach (var card in cards)
        {
            var take = card.IsRow ? 15 : 4;

            var products = card.CategoryLinks
                .Select(cl => cl.CategoryId)
                .Where(byCategory.ContainsKey)
                .SelectMany(id => byCategory[id])
                .Take(take)
                .ToList();

            result.Add(ToCardDto(card, products));
        }

        return result;
    }

    // ---------- Group cards helper (NEW) ----------

    private async Task<List<HomeProductCardDto>> BuildGroupCards(
        List<HomePageCard> cards,
        IQueryable<Product> baseQuery)
    {
        var result = new List<HomeProductCardDto>();
        if (cards.Count == 0) return result;

        // 1. Cards → group memberships (one query)
        var groupIds = cards.Select(c => c.UserGroupId).Distinct().ToList();

        var membership = await _context.GroupMembers
            .AsNoTracking()
            .Where(g => groupIds.Contains(g.UserGroupId))
            .Select(g => new { g.UserGroupId, g.UserProfileId })
            .ToListAsync();

        var membersByGroup = membership
            .GroupBy(m => m.UserGroupId)
            .ToDictionary(g => g.Key, g => g.Select(m => m.UserProfileId).ToList());

        var allUserProfileIds = membership
            .Select(m => m.UserProfileId)
            .Distinct()
            .ToList();

        if (allUserProfileIds.Count == 0)
        {
            foreach (var card in cards)
                result.Add(ToCardDto(card, new List<HomePageProductDto>()));
            return result;
        }

        // 2. UserProfile.Id → SellerProfileId (one query)
        var sellerLinks = await _context.SellerProfiles
            .AsNoTracking()
            .Where(s => allUserProfileIds.Contains(s.SellerId))
            .Select(s => new { s.SellerId, s.SellerProfileId })
            .ToListAsync();

        // One user may own multiple seller profiles — flatten to a list.
        var sellerProfileIdsByUserId = sellerLinks
            .GroupBy(s => s.SellerId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.SellerProfileId).ToList());

        var allSellerProfileIds = sellerLinks
            .Select(s => s.SellerProfileId)
            .Distinct()
            .ToList();

        if (allSellerProfileIds.Count == 0)
        {
            foreach (var card in cards)
                result.Add(ToCardDto(card, new List<HomePageProductDto>()));
            return result;
        }

        // 3. SellerProfileId → featured Products (one query)
        var productRows = await baseQuery
            .Where(p => p.IsFeatured && allSellerProfileIds.Contains(p.SellerProfileId))
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new
            {
                p.SellerProfileId,
                Product = new HomePageProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    OldPrice = p.OldPrice,
                    SlugName = p.Slug,
                    SellerProfileId = p.SellerProfileId,
                    SellerName = p.SellerProfile != null
                        ? p.SellerProfile.SellerName : UnknownSeller,
                    SellerSlugName = p.SellerProfile != null
                        ? p.SellerProfile.Slug : UnknownSeller,
                    ProductImageId = p.Images
                        .OrderByDescending(i => i.IsPrimary)
                        .Select(i => i.ProductImageId)
                        .FirstOrDefault()
                }
            })
            .ToListAsync();

        var productsBySeller = productRows
            .GroupBy(x => x.SellerProfileId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Product).ToList());

        // 4. Assemble cards in original index order
        foreach (var card in cards)
        {
            var take = card.IsRow ? 15 : 4;

            var memberIds = membersByGroup.TryGetValue((Guid)card.UserGroupId, out var mids)
                ? mids
                : new List<Guid>();

            var products = memberIds
                .Where(sellerProfileIdsByUserId.ContainsKey)
                .SelectMany(uid => sellerProfileIdsByUserId[uid])
                .Where(productsBySeller.ContainsKey)
                .SelectMany(spid => productsBySeller[spid])
                .Take(take)
                .ToList();

            result.Add(ToCardDto(card, products));
        }

        return result;
    }

    private static HomeProductCardDto ToCardDto(
        HomePageCard card,
        List<HomePageProductDto> products) => new()
        {
            HomePageCardId = card.HomePageCardId,
            Title = card.Title,
            Color = card.Color,
            LinkLabel = card.LinkLabel,
            Index = card.Index,
            IsRow = card.IsRow,
            SellerSlugName=card.SellerSlugName??"",
            Products = products
        };

    // ============================================================
    //  Group members' products
    // ============================================================

    public async Task<ICollection<SellerProductsListDto>> GetGroupMembersProducts(
        IList<Guid> groupMemberIds)
    {
        if (groupMemberIds == null || groupMemberIds.Count == 0) return [];

        var hash = ComputeHash(groupMemberIds);

        return await GetOrCreateCacheAsync(CacheKeys.GroupMembers(hash), async () =>
        {
            var sellerIds = groupMemberIds.ToHashSet();

            var products = await BaseQuery()
                .Where(p => p.IsActive && sellerIds.Contains(p.SellerProfileId))
                .Select(p => new
                {
                    p.SellerProfileId,
                    Product = new SellerProductDto
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Condition = p.Condition,
                        SlugName = p.Slug,
                        SellerName = p.SellerProfile != null
                            ? p.SellerProfile.SellerName : UnknownSeller,
                        SellerSlugName = p.SellerProfile != null
                            ? p.SellerProfile.Slug : UnknownSeller,
                        CategoryName = p.Category != null
                            ? p.Category.CategoryName : DefaultCategory,
                        ReviewSummary = p.ProductReviews.Any()
                            ? (int)p.ProductReviews.Average(r => r.Rating)
                            : 0,
                        ProductImageId = p.Images
                            .OrderByDescending(i => i.IsPrimary)
                            .Select(i => i.ProductImageId)
                            .FirstOrDefault()
                    }
                })
                .ToListAsync();

            return products
                .GroupBy(x => x.SellerProfileId)
                .Select(g => new SellerProductsListDto
                {
                    SellerProducts = g.Select(x => x.Product).ToList()
                })
                .ToList();
        }) ?? [];
    }

    public async Task<ICollection<SellerProductsListDto>> GetGroupMembersForDisplayProducts(
        IList<Guid> groupMemberIds, Guid groupId)
    {
        if (groupMemberIds == null || groupMemberIds.Count == 0) return [];

        const int MaxProducts = 50;

        return await GetOrCreateCacheAsync(CacheKeys.GroupProducts(groupId), async () =>
        {
            var featuredIds = await _context.GroupFeaturedProducts
                .AsNoTracking()
                .Where(x => x.UserGroupId == groupId)
                .Select(x => x.ProductId)
                .Take(MaxProducts)
                .ToListAsync();

            var sellerIds = groupMemberIds.ToHashSet();

            var products = await BaseQuery()
                .Where(p => p.IsActive && sellerIds.Contains(p.SellerProfileId))
                .OrderByDescending(p => featuredIds.Contains(p.ProductId))
                .ThenByDescending(p => p.CreatedAt)
                .Take(MaxProducts)
                .Select(p => new
                {
                    p.SellerProfileId,
                    Product = new SellerProductDto
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Condition = p.Condition,
                        SlugName = p.Slug,
                        SellerName = p.SellerProfile != null
                            ? p.SellerProfile.SellerName : UnknownSeller,
                        SellerSlugName = p.SellerProfile != null
                            ? p.SellerProfile.Slug : UnknownSeller,
                        CategoryName = p.Category != null
                            ? p.Category.CategoryName : DefaultCategory,
                        ReviewSummary = p.ProductReviews.Any()
                            ? (int)p.ProductReviews.Average(r => r.Rating)
                            : 0,
                        ProductImageId = p.Images
                            .OrderByDescending(i => i.IsPrimary)
                            .Select(i => i.ProductImageId)
                            .FirstOrDefault()
                    }
                })
                .ToListAsync();

            return products
                .GroupBy(x => x.SellerProfileId)
                .Select(g => new SellerProductsListDto
                {
                    SellerProducts = g.Select(x => x.Product).ToList()
                })
                .ToList();
        }, 30) ?? [];
    }

    // ============================================================
    //  Helpers
    // ============================================================

    private static string ComputeHash(IList<Guid> ids)
    {
        var ordered = ids.OrderBy(x => x);
        using var md5 = MD5.Create();
        var bytes = ordered.SelectMany(x => x.ToByteArray()).ToArray();
        return Convert.ToHexString(md5.ComputeHash(bytes));
    }

    // ProductRepo
    public Task<List<SlugInfo>> GetAllProductSlugsAsync() =>
        BaseQuery()
            .Where(p => p.IsActive)
            .Select(p => new SlugInfo
            {
                Slug = p.Slug,
                UpdatedAt =  p.CreatedAt
            })
            .ToListAsync();

    public Task<ProductPreviewDto?> FindProductBySlugForPreviewAsync(string slug) =>
    BaseQuery()
        .Where(p => p.Slug == slug && p.IsActive)
        .Select(p => new ProductPreviewDto
        {
            ProductId = p.ProductId,
            Slug = p.Slug,
            ProductName = p.ProductName,
            Description = p.Description,            
            Price = p.Price,
            SellerName = p.SellerProfile != null ? p.SellerProfile.SellerName : null,
            PrimaryImageId = p.Images
                .OrderByDescending(i => i.IsPrimary)
                .Select(i => i.ProductImageId.ToString())
                .FirstOrDefault()
        })
        .FirstOrDefaultAsync();

    // Repeat the pattern in SellerRepo and CategoryRepo
}