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

namespace Repository.Repos;

public sealed class TradeRepo : RepositoryBase<Trade>, ITradeRepo
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _cache;


    private static CancellationTokenSource _tradeListCacheReset = new();

    private const string UnknownSeller = "Unknown Seller";
    private const string UnknownSlug = "unknown";
    private const string DefaultCategory = "Not Categorised";

    public TradeRepo(
        ApplicationDbContext context,
        IMemoryCache cache) : base(context)
    {
        _context = context;
        _cache = cache;
    }

    #region Base Query

    private IQueryable<Trade> BaseQuery(bool tracking = false)
    {
        return tracking
            ? FindAll(true)
            : FindAll(false).AsNoTracking();
    }

    #endregion

    #region Cache

    private static class CacheKeys
    {
        public static string Trade(Guid id)
            => $"trade:{id}";

        public static string TradeSlug(string slug)
            => $"trade:slug:{slug}";

        public static string GroupTrades(Guid groupId)
            => $"group-trades:{groupId}";

        public static string HomePageTrades(
            ProductRequestParameters p)
            => $"homepage-trades:{p.CategoryId}:{p.SubCategoryId}:{p.ProductName}:{p.PageNumber}:{p.PageSize}";
    }

    private async Task<T?> GetOrCreateCacheAsync<T>(
        string key,
        Func<Task<T?>> factory,
        int minutes = 20,
        bool useGlobalToken = false)
    {
        return await _cache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(minutes);

                if (useGlobalToken)
                {
                    entry.AddExpirationToken(
                        new CancellationChangeToken(
                            _tradeListCacheReset.Token));
                }

                return await factory();
            });
    }

    private void ResetTradeListCaches()
    {
        var old = Interlocked.Exchange(
            ref _tradeListCacheReset,
            new CancellationTokenSource());

        old.Cancel();
        old.Dispose();
    }

    private void InvalidateSingleTradeCache(
        Guid tradeId,
        string? slug)
    {
        _cache.Remove(CacheKeys.Trade(tradeId));

        if (!string.IsNullOrWhiteSpace(slug))
        {
            _cache.Remove(
                CacheKeys.TradeSlug(slug));
        }
    }

    #endregion

    #region Projections

    private static readonly Expression<Func<Trade, HomePageTradeDto>>
        HomeTradeProjection = t => new HomePageTradeDto
        {
            TradeId = t.TradeId,
            TradeName = t.TradeName,
            Description = t.Description,

            SellerName = t.SellerProfile.SellerName,

            SellerProfileId = t.SellerProfileId,

            SellerSlugName =
                t.SellerProfile.Slug,

            TradeSlugName = t.Slug,

            TradeImageId = t.Images
                .OrderByDescending(i => i.IsPrimary)
                .Select(i => i.TradeImageId)
                .FirstOrDefault()
        };

    private static readonly Expression<Func<Trade, SellerTradeDto>>
        SellerTradeProjection = t => new SellerTradeDto
        {
            TradeId = t.TradeId,
            TradeName = t.TradeName,
            Description = t.Description,

            TradeSlugName = t.Slug,

            SellerSlugName =
                t.SellerProfile.Slug,

            SellerName =
                t.SellerProfile.SellerName,

            WhatsAppNumber =
                t.SellerProfile.WhatsAppNumber,

            Category = t.Category != null
                ? t.Category.CategoryName
                : DefaultCategory,

            CategoryName = t.Category != null
                ? t.Category.CategoryName
                : DefaultCategory,

            ReviewSummary =
                t.TradeReviews.Any()
                    ? (int)t.TradeReviews
                        .Average(r => r.Rating)
                    : 0,

            TradeImageId = t.Images
                .OrderByDescending(i => i.IsPrimary)
                .Select(i => i.TradeImageId)
                .FirstOrDefault()
        };

    private static readonly Expression<Func<Trade, ShowTradeDataDto>>
        ShowTradeProjection = t => new ShowTradeDataDto
        {
            TradeId = t.TradeId,
            TradeName = t.TradeName,

            Category = t.Category != null
                ? t.Category.CategoryName
                : DefaultCategory,

            CreatedAt = t.CreatedAt,

            HasImage = t.HasImage,

            SellerProfileId = t.SellerProfileId,

            SellerName =
                t.SellerProfile.SellerName,

            SellerSlugName =
                t.SellerProfile.Slug,

            TradeSlugName = t.Slug,

            WhatsAppNumber =
                t.SellerProfile.WhatsAppNumber,

            Reviews = new List<ShowReviewDto>(),

            TradeImageRefs = t.Images
                .Select(i => new TradeImageRefDto
                {
                    TradeImageId = i.TradeImageId,
                    IsPrimary = i.IsPrimary,
                    IsProcessed = i.IsProcessed
                })
                .ToList()
        };

    #endregion

    #region CRUD

    public Guid CreateTrade(Trade trade)
    {
        CreateBase(trade);

        ResetTradeListCaches();

        return trade.TradeId;
    }

    public void UpdateTrade(Trade trade)
    {
        UpdateBase(trade);

        InvalidateSingleTradeCache(
            trade.TradeId,
            trade.Slug);

        ResetTradeListCaches();
    }

    public void DeleteTrade(Trade trade)
    {
        DeleteBase(trade);

        InvalidateSingleTradeCache(
            trade.TradeId,
            trade.Slug);

        ResetTradeListCaches();
    }

    #endregion

    #region Featured

    public Task MakeAllTradesFeautured()
    {
        ResetTradeListCaches();

        return BaseQuery(true)
            .Where(t => !t.IsFeatured)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    p => p.IsFeatured,
                    true));
    }

    public async Task MakeTradeFeautured(Guid tradeId)
    {
        var slug = await BaseQuery()
            .Where(t => t.TradeId == tradeId)
            .Select(t => t.Slug)
            .FirstOrDefaultAsync();

        await BaseQuery(true)
            .Where(t => t.TradeId == tradeId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    p => p.IsFeatured,
                    true));

        InvalidateSingleTradeCache(
            tradeId,
            slug);

        ResetTradeListCaches();
    }

    #endregion

    #region Basic Queries

    public Task<int> NumberOfTrades()
        => BaseQuery()
            .CountAsync();
    //public async Task<long> NextTradeSlugNumberAsync() =>
    //await _context.Database.SqlQuery<long>(
    //    $"SELECT NEXT VALUE FOR TradeSlugSeq").SingleAsync();




    public async Task<long> NextTradeSlugNumberAsync()
    {
        var connection = _context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        await using var command = connection.CreateCommand();

        command.CommandText = "SELECT NEXT VALUE FOR TradeSlugSeq";

        var result = await command.ExecuteScalarAsync();

        return Convert.ToInt64(result);
    }





    public Task<Guid> GetTradeIdBySlugName(
        string slug)
        => BaseQuery()
            .Where(t => t.Slug == slug)
            .Select(t => t.TradeId)
            .FirstOrDefaultAsync();

    public Task<Trade?> FindTradeForUpdate(
        Guid tradeId)
        => BaseQuery(true)
            .FirstOrDefaultAsync(
                t => t.TradeId == tradeId);

    #endregion

    #region Homepage Trades

    public async Task<PagedList<HomePageTradeDto>>
        GetHomePageTrades(
            ProductRequestParameters request)
    {
        var cacheKey =
            CacheKeys.HomePageTrades(request);

        var cached =
            await GetOrCreateCacheAsync(
                cacheKey,
                async () =>
                {
                    var query = BaseQuery()
                        .Where(t =>
                            t.IsActive);

                    if (request.CategoryId>0)
                    {
                        query = query.Where(t =>
                            t.CategoryId ==
                            request.CategoryId.Value);
                    }

                    if (request.SubCategoryId > 0)
                    {
                        query = query.Where(t =>
                            t.SubCategoryId ==
                            request.SubCategoryId.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(
                            request.ProductName))
                    {
                        var search =
                            request.ProductName.Trim();

                        query = query.Where(t =>
                            EF.Functions.Like(
                                t.TradeName,
                                $"%{search}%"));
                    }

                    query = query
                        .OrderByDescending(
                            t => t.CreatedAt);

                    var count =
                        await query.CountAsync();

                    var items = await query
                        .Skip(
                            (request.PageNumber - 1) *
                            request.PageSize)
                        .Take(request.PageSize)
                        .Select(HomeTradeProjection)
                        .ToListAsync();

                    return PagedList<HomePageTradeDto>
                        .ToPagedList(
                            items,
                            request.PageNumber,
                            request.PageSize);
                },
                60,
                true);

        return cached!;
    }

    #endregion

    #region Trade Details

    public async Task<TradeDataDto?> GetTradeData(
        Guid tradeId)
    {
        var cacheKey =
            CacheKeys.Trade(tradeId);

        return await GetOrCreateCacheAsync(
            cacheKey,
            async () =>
            {
                var result = await BaseQuery()
                    .Where(t =>
                        t.TradeId == tradeId &&
                        t.IsActive &&
                        !t.IsDeleted)
                    .Select(t => new
                    {
                        Trade = new TradeDataDto
                        {
                            TradeId = t.TradeId,
                            TradeName = t.TradeName,

                            Impressions =
                                t.TradeImpressions.Count(),

                            TradeSlugName = t.Slug,

                            SellerSlugName =
                                t.SellerProfile.Slug,

                            WhatsAppNumber =
                                t.SellerProfile
                                    .WhatsAppNumber,

                            Description =
                                t.Description,

                            CreatedAt =
                                t.CreatedAt,

                            SellerProfileId =
                                t.SellerProfileId,

                            SellerUserProfileId =
                                t.SellerProfile
                                    .SellerId,

                            SellerName =
                                t.SellerProfile
                                    .SellerName,

                            Category =
                                t.Category != null
                                    ? t.Category.CategoryName
                                    : DefaultCategory,

                            Reviews = t.TradeReviews
                                .Select(r =>
                                    new ShowReviewDto
                                    {
                                        ReviewId =
                                            r.TradeReviewId,

                                        ReviewerName =
                                            r.Reviewer != null
                                                ? (
                                                    r.Reviewer
                                                        .IdentityUser
                                                        .FirstName +
                                                    " " +
                                                    r.Reviewer
                                                        .IdentityUser
                                                        .LastName
                                                  ).Trim()
                                                : "Unknown",

                                        Rating = r.Rating,
                                        Comment = r.Comment,
                                        CreatedAt =
                                            r.CreatedAt
                                    })
                                .ToList(),

                            TradeImageRefDtos =
                                t.Images
                                    .Select(i =>
                                        new TradeImageRefDto
                                        {
                                            TradeImageId =
                                                i.TradeImageId,

                                            IsPrimary =
                                                i.IsPrimary,

                                            IsProcessed =
                                                i.IsProcessed
                                        })
                                    .ToList()
                        },

                        t.CategoryId
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                    return null;

                result.Trade.RelatedTrades =
                    await BaseQuery()
                        .Where(t =>
                            t.IsActive &&
                            !t.IsDeleted &&
                            t.CategoryId ==
                            result.CategoryId &&
                            t.TradeId != tradeId)
                        .OrderByDescending(
                            t => t.CreatedAt)
                        .Take(6)
                        .Select(HomeTradeProjection)
                        .ToListAsync();

                return result.Trade;
            },
            15);
    }

    public async Task<TradeDataDto?>
        GetTradeDataUsingSlugName(
            string slug)
    {
        var cacheKey =
            CacheKeys.TradeSlug(slug);

        return await GetOrCreateCacheAsync(
            cacheKey,
            async () =>
            {
                var tradeId = await BaseQuery()
                    .Where(t => t.Slug == slug)
                    .Select(t => t.TradeId)
                    .FirstOrDefaultAsync();

                if (tradeId == Guid.Empty)
                    return null;

                return await GetTradeData(tradeId);
            },
            15);
    }

    #endregion

    #region Seller Trades

    public Task<ShowTradeDataDto?>
        FindSellerTrade(Guid tradeId)
    {
        return BaseQuery()
            .Where(t => t.TradeId == tradeId)
            .Select(ShowTradeProjection)
            .FirstOrDefaultAsync();
    }

    public Task<ShowTradeDataDto?>
        FindSellerTradeUsingSlugName(
            bool tracking,
            string slug)
    {
        return BaseQuery(tracking)
            .Where(t => t.Slug == slug)
            .Select(ShowTradeProjection)
            .FirstOrDefaultAsync();
    }

    public Task<ShowTradeDataDto?>
        FindTradeBySlugName(
            bool tracking,
            string slug)
    {
        return BaseQuery(tracking)
            .Where(t => t.Slug == slug)
            .Select(ShowTradeProjection)
            .FirstOrDefaultAsync();
    }

    #endregion



    public Task<List<SlugInfo>> GetAllTradesSlugsAsync() => BaseQuery().Where(p => p.IsActive).Select(p => new SlugInfo{Slug = p.Slug,UpdatedAt = p.CreatedAt })         .ToListAsync();



    #region Group Trades

    public async Task<ICollection<SellerTradeListDto>>
        GetGroupMembersTrades(
            IList<Guid> groupMemberIds,
            Guid groupId)
    {
        if (groupMemberIds == null ||
            groupMemberIds.Count == 0)
        {
            return [];
        }

        var cacheKey =
            CacheKeys.GroupTrades(groupId);

        return await GetOrCreateCacheAsync(
            cacheKey,
            async () =>
            {
                var sellerIds =
                    groupMemberIds.ToHashSet();

                var trades = await BaseQuery()
                    .Where(t =>
                        t.IsActive &&
                        !t.IsDeleted &&
                        sellerIds.Contains(
                            t.SellerProfileId))
                    .OrderByDescending(
                        t => t.CreatedAt)
                    .Select(t => new
                    {
                        t.SellerProfileId,

                        Trade =
                            new SellerTradeDto
                            {
                                TradeId = t.TradeId,
                                TradeName =
                                    t.TradeName,

                                Description =
                                    t.Description,

                                TradeSlugName =
                                    t.Slug,

                                SellerSlugName =
                                    t.SellerProfile
                                        .Slug,

                                SellerName =
                                    t.SellerProfile
                                        .SellerName,

                                WhatsAppNumber =
                                    t.SellerProfile
                                        .WhatsAppNumber,

                                Category =
                                    t.Category != null
                                        ? t.Category
                                            .CategoryName
                                        : DefaultCategory,

                                CategoryName =
                                    t.Category != null
                                        ? t.Category
                                            .CategoryName
                                        : DefaultCategory,

                                ReviewSummary =
                                    t.TradeReviews.Any()
                                        ? (int)t
                                            .TradeReviews
                                            .Average(r =>
                                                r.Rating)
                                        : 0,

                                TradeImageId =
                                    t.Images
                                        .OrderByDescending(i =>
                                            i.IsPrimary)
                                        .Select(i =>
                                            i.TradeImageId)
                                        .FirstOrDefault()
                            }
                    })
                    .ToListAsync();

                return trades
                    .GroupBy(x =>
                        x.SellerProfileId)
                    .Select(g =>
                        new SellerTradeListDto
                        {
                            SellerTrades =
                                g.Select(x => x.Trade)
                                    .ToList()
                        })
                    .ToList();
            },
            20,
            true) ?? [];
    }

    #endregion
}