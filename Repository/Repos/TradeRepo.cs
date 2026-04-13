using Contracts.Repo;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class TradeRepo : RepositoryBase<Trade>, ITradeRepo
    {
        public TradeRepo(ApplicationDbContext _db) : base(_db)
        {

        }


        public async Task<PagedList<HomePageTradeDto>> GetHomePageTrades(ProductRequestParameters requestParameters)
        {

            var query = FindAll(false)
               .Where(p => p.IsActive && p.IsFeatured && !p.IsDeleted && p.SellerProfile != null ? p.SellerProfile.SellerTypeId == 1 : true);

            // 🔎 Filter by Category
            if (requestParameters.CategoryId.HasValue && requestParameters.CategoryId.Value > 0)
            {
                var category = requestParameters.CategoryId;
                query = query.Where(p =>
                    p.Category != null &&
                    p.Category.CategoryId == category);
            }


            // 🔎 Filter by SubCategory
            if (requestParameters.SubCategoryId.HasValue && requestParameters.SubCategoryId.Value > 0)
            {

                var subCategory = requestParameters.SubCategoryId;
                query = query.Where(p =>
                    p.SubCategory != null &&
                    p.SubCategory.SubCategoryId == subCategory);

            }
          

            // 🔤 Product Name Search
            if (!string.IsNullOrWhiteSpace(requestParameters.ProductName))
            {
                var name = requestParameters.ProductName.Trim().ToLower();
                query = query.Where(p => p.TradeName.ToLower().Contains(name));
            }

            query = query.OrderByDescending(p => p.CreatedAt);

            var count = await query.CountAsync();

            var items = await query
                .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
                .Take(requestParameters.PageSize)
                .Select(p => new HomePageTradeDto
                {
                    TradeId = p.TradeId,                  
                    TradeName = p.TradeName,
                    Description = p.Description,
                    SellerName = p.SellerProfile != null
                        ? p.SellerProfile.SellerName
                        : "Unknown",
                    SellerProfileId = p.SellerProfileId,
                    TradeImageId = p.Images
                        .OrderByDescending(i => i.IsPrimary)
                        .Select(i => i.TradeImageId)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return PagedList<HomePageTradeDto>.ToPagedList(items, requestParameters.PageNumber, requestParameters.PageSize);
        }



        public async Task<TradeDataDto?> GetTradeData(Guid tradeId)
        {
            var result = await FindByCondition(t => t.TradeId == tradeId, false)
                .Select(t => new
                {
                    Trade = new TradeDataDto
                    {
                        TradeId = t.TradeId,
                        TradeName = t.TradeName,
                        Description = t.Description,                       
                        CreatedAt = t.CreatedAt,
                        SellerProfileId = t.SellerProfileId,
                        SellerUserProfileId = t.SellerProfile != null ? t.SellerProfile.SellerId : Guid.Empty,

                        SellerName = t.SellerProfile != null
                            ? t.SellerProfile.SellerName
                            : "Unknown Seller",
                        Category = t.Category != null
                            ? t.Category.CategoryName
                            : "Not Categorised",               
                        Reviews = t.TradeReviews
                            .Select(r => new ShowReviewDto
                            {
                                ReviewId = r.TradeReviewId,
                                ReviewerName = r.Reviewer != null
                                    ? r.Reviewer.IdentityUser.FirstName + " " + r.Reviewer.IdentityUser.LastName
                                    : "Unknown",
                                Rating = r.Rating,
                                Comment = r.Comment,
                                CreatedAt = r.CreatedAt
                            })
                            .ToList(),

                        TradeImageRefDtos = t.Images
                            .Select(i => new TradeImageRefDto
                            {
                                TradeImageId = i.TradeImageId,
                                IsPrimary = i.IsPrimary,
                                IsProcessed = i.IsProcessed
                            })
                            .ToList()
                    },
                    t.CategoryId
                })
                .FirstOrDefaultAsync();

            if (result == null)
                return null;

            var relatedTrades = await FindByCondition(
                    t => t.CategoryId == result.CategoryId && t.TradeId != tradeId,
                    false)
                .Select(t => new HomePageTradeDto
                {
                    TradeId = t.TradeId,
                    TradeName = t.TradeName,                   
                    SellerName = t.SellerProfile != null
                        ? t.SellerProfile.SellerName
                        : "Unknown",
                    SellerProfileId = t.SellerProfileId,
                    TradeImageId = t.Images
                        .OrderByDescending(i => i.IsPrimary)
                        .Select(i => i.TradeImageId)
                        .FirstOrDefault()
                })
                .Take(6)
                .ToListAsync();

            result.Trade.RelatedTrades = relatedTrades;

            return result.Trade;
        }


        public Task<Trade?> FindTradeForUpdate(Guid tradeId)
        {
            return FindByCondition(p => p.TradeId == tradeId, true).FirstOrDefaultAsync();
        }
        public Guid CreateTrade(Trade trade)
        {
            CreateBase(trade);
            return trade.TradeId;
        }
        public void UpdateTrade(Trade trade) => UpdateBase(trade);
        public void DeleteTrade(Trade trade) => DeleteBase(trade);


        public async Task<ShowTradeDataDto?> FindSellerTrade(Guid tradeId)
        {
            var tradeQ = FindByCondition(p => p.TradeId == tradeId, false);

            return await tradeQ
                .Select(p => new ShowTradeDataDto
                {
                    TradeId = p.TradeId,
                    TradeName = p.TradeName,                 
                    TradeBookings = p.TradeBookings,
                    Category = p.Category != null ? p.Category.CategoryName : "Not Categorised",
                    CreatedAt = p.CreatedAt,
                    HasImage = p.HasImage,
                    SellerProfileId = p.SellerProfileId,
                    SellerName = p.SellerProfile != null ? $"{p.SellerProfile.SellerName}" : "Unkown Seller",
                    
                    Reviews = p.TradeReviews.Count != 0 ? p.TradeReviews.Select(r => new ShowReviewDto
                    {
                        ReviewId = r.TradeReviewId,
                        ReviewerName = string.Join(" ", r.Reviewer != null ? r.Reviewer.IdentityUser.FirstName : "Unknown", r.Reviewer != null ? r.Reviewer.IdentityUser.LastName : "Unknown").Trim(),
                        Rating = r.Rating,
                        Comment = r.Comment,
                        CreatedAt = r.CreatedAt
                    }).ToList() : new List<ShowReviewDto>(),

                    TradeImageRefs = p.Images.Select(i => new TradeImageRefDto
                    {
                        TradeImageId = i.TradeImageId,
                        IsPrimary = i.IsPrimary,
                        IsProcessed = i.IsProcessed,
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
                

        public async Task<ICollection<SellerTradeListDto>> GetGroupMembersTrades(IList<Guid> groupMemberIds)
        {
            if (groupMemberIds == null || !groupMemberIds.Any())
                return [];

            var trades = await FindByCondition(p => p.SellerProfileId!=Guid.Empty &&
                         groupMemberIds.Contains(p.SellerProfileId) &&
                         !p.IsDeleted &&
                         p.IsActive,
                    false)
                .Select(t => new
                {
                    SellerId = t.SellerProfileId,
                    Trade = new SellerTradeDto
                    {
                        TradeId = t.TradeId,
                        TradeName = t.TradeName,
                        Description = t.Description,
                        Category = t.Category != null ? t.Category.CategoryName : "Not Categorised",
                        CategoryName = t.Category != null ? t.Category.CategoryName : "Not Categorised",
                        ReviewSummary = t.TradeReviews.Any() ? (int)t.TradeReviews.Average(r => r.Rating) : 0,
                        TradeImageId = t.Images
                            .OrderByDescending(i => i.IsPrimary)
                            .Select(i => i.TradeImageId)
                            .FirstOrDefault(),                    
                        SellerName = t.SellerProfile != null
                            ? t.SellerProfile.SellerName
                            : "Unknown Seller"
                    }
                })
                .ToListAsync();

            var result = trades
                .GroupBy(t => t.SellerId)
                .Select(g => new SellerTradeListDto
                {
                    SellerTrades = g.Select(x => x.Trade).ToList()
                })
                .ToList();

            return result.Count>0 ? result : [];
        }

        public void MakeAllTradesFeautured()
        {
            var query = FindAll(false).Where(p => !p.IsFeatured).ExecuteUpdateAsync(setters => setters.SetProperty(p => p.IsFeatured, true));
        }

        public void MakeTradeFeautured(Guid tradeId)
        {
            var query = FindByCondition(p => p.TradeId == tradeId, false).ExecuteUpdateAsync(setters => setters.SetProperty(p => p.IsFeatured, true));

        }
      
    }
}

