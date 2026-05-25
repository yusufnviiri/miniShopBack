using Contracts.Repo;
using Entities.Models;
using Lucene.Net.Store;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class SellerProfileRepo:RepositoryBase<SellerProfile>,ISellerProfileRepo
    {
        private readonly ApplicationDbContext _context;
        public SellerProfileRepo(ApplicationDbContext dbContext):base(dbContext)
        {
            _context = dbContext;

        }
        public async Task<Guid> GetSellerId(Guid sellerProfile)
        {
            return await FindByCondition(sp => sp.SellerProfileId == sellerProfile, false)
                .Select(sp => sp.SellerId)
                .FirstOrDefaultAsync();
        }
        public async Task<Guid> GetSellerProfileId(Guid sellerId)
        {
            return await FindByCondition(sp => sp.SellerId == sellerId, false)
               .Select(sp => sp.SellerProfileId)
               .FirstOrDefaultAsync();
        }


        public async   Task<IEnumerable<SellerProfileDto>> GetAllSellerProfiles()
        {
            return await FindAll(false)
                .Select(sp => new SellerProfileDto
                {
                    SellerProfileId = sp.SellerProfileId,
                    SellerId = sp.SellerId,
                    SellerTypeId = sp.SellerTypeId,
                    SellerType = sp.SellerType,
                    SellerPolicyId = sp.SellerPolicyId,
                    SellerPolicy = sp.SellerPolicy,
                    SellerTierId = sp.SellerTierId,
                    SellerRestrictions = sp.SellerRestrictions,
                    SellerName = sp.SellerName,
                    WhatsAppNumber = sp.WhatsAppNumber,

                }).ToListAsync();
        }
        public async Task<SellerProfile?> FindSellerProfileById(Guid sellerProfileId, bool tracking)
        {
            return await FindByCondition(sp => sp.SellerProfileId == sellerProfileId, tracking)
                .FirstOrDefaultAsync();
        }


        public Guid CreateSellerProfile(SellerProfile sellerProfile) { CreateBase(sellerProfile);
            return sellerProfile.SellerProfileId;
        
        }
        public void UpdateSellerProfile(SellerProfile sellerProfile)=>UpdateBase(sellerProfile);
        public void DeleteSellerProfile(SellerProfile sellerProfile)=>DeleteBase(sellerProfile);
        public async Task<bool> CheckifUserIsSeller(Guid userProfileId)
        {
            return await FindByCondition(sp => sp.SellerId == userProfileId, false)
                .AnyAsync();
        }


        public async Task<bool> CheckifUserGroupIsSeller(Guid userGroupId)
        {
            return await FindByCondition(sp => sp.SellerId == userGroupId, false)
                .AnyAsync();
        }
        public async Task<SellerProfile?> FindSellerProfileBySellerId(Guid sellerId)
        {
            return await FindByCondition(sp => sp.SellerId == sellerId, false)
                .FirstOrDefaultAsync();
        }
        public async Task<SellerShopDto?> GetSellerShopDetails(Guid sellerProfileId) {
            return await FindByCondition(p => p.SellerProfileId == sellerProfileId, false).Select(s => new SellerShopDto()
            {
                SellerProfileId = s.SellerProfileId,
                SellerId=s.SellerId,
                SellerName = s.SellerName,
                WhatsAppNumber =s.WhatsAppNumber,   
                SellerSlugName=s.Slug,
                SellerTypeDescription = s.SellerType != null ? s.SellerType.SellerTypeName : "Not Categorized",
                Products = s.Products.Any() ? s.Products.Select(p => new SellerProductDto()
                {
                    ProductId = p.ProductId,
                    Price = p.Price,
                    SlugName=p.Slug,
                    ProductName = p.ProductName,
                    SellerSlugName = p.SellerProfile != null ? $"{p.SellerProfile.Slug}" : "Unkown Seller",
                    CategoryName = p.Category != null ? p.Category.CategoryName : "Not Categorized",
                    ReviewSummary = p.ProductReviews.Count != 0 ? (int) p.ProductReviews.Average(r => r.Rating) : 0,
                    ProductImageId = p.Images.OrderByDescending(i => i.IsPrimary).Select(i => i.ProductImageId).FirstOrDefault(),
                }).ToList() : new List<SellerProductDto>(),
                Trades= s.Trades.Any() ? s.Trades.Select(p => new SellerTradeDto()
                {
                    TradeId = p.TradeId,
                    TradeSlugName=p.Slug,
               
                    TradeName = p.TradeName,
                    Description = p.Description,
                        SellerSlugName = p.SellerProfile != null ? $"{p.SellerProfile.Slug}" : "Unkown Seller",
                    TradeImageId = p.Images.OrderByDescending(i => i.IsPrimary).Select(i => i.TradeImageId).FirstOrDefault(),
                        CategoryName = p.Category != null ? p.Category.CategoryName : "Not Categorized",
                        ReviewSummary = p.TradeReviews.Count != 0 ? (int)p.TradeReviews.Average(r => r.Rating) : 0,
                        }).ToList() : new List<SellerTradeDto>(),     
                }).FirstOrDefaultAsync();
                }


        public async Task<MiniGroupDetailsDto?> FindMiniGroupDetailsById(Guid sellerProfileId)
        {
           return await FindByCondition(p=>p.SellerProfileId==sellerProfileId,false).Select(p => new MiniGroupDetailsDto()
            {
                SellerProfileId = sellerProfileId,
                SellerId = p.SellerId,
                SellerName = p.SellerName,
                SlugName=p.Slug,
                SellerTypeDescription = p.SellerType!=null? p.SellerType.SellerTypeName:"not categorized"

            }).FirstOrDefaultAsync();
            

        }

        public async Task<GroupProductsAndTradesList?> GetGroupProductsAndTradesList(Guid sellerProfileId)
        {

            return await FindByCondition(p => p.SellerProfileId == sellerProfileId, false).Select(s => new GroupProductsAndTradesList()
            {
              
                GroupProducts = s.Products.Any() ? s.Products.Select(p => new SellerProductDto()
                {
                    ProductId = p.ProductId,
                    Price = p.Price,
                    SlugName=p.Slug,
                    ProductName = p.ProductName,
                    CategoryName = p.Category != null ? p.Category.CategoryName : "Not Categorized",
                    ReviewSummary = p.ProductReviews.Count != 0 ? (int)p.ProductReviews.Average(r => r.Rating) : 0,
                    ProductImageId = p.Images.OrderByDescending(i => i.IsPrimary).Select(i => i.ProductImageId).FirstOrDefault(),
                }).ToList() : new List<SellerProductDto>(),
                GroupTrades = s.Trades.Any() ? s.Trades.Select(p => new SellerTradeDto()
                {
                    TradeId = p.TradeId,
                
                    TradeName = p.TradeName,
                    Description = p.Description,
                    TradeSlugName=p.Slug,
                    TradeImageId = p.Images.OrderByDescending(i => i.IsPrimary).Select(i => i.TradeImageId).FirstOrDefault(),
                    CategoryName = p.Category != null ? p.Category.CategoryName : "Not Categorized",
                    ReviewSummary = p.TradeReviews.Count != 0 ? (int)p.TradeReviews.Average(r => r.Rating) : 0,
                }).ToList() : new List<SellerTradeDto>(),
            }).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Guid>> GetGroupMemberSellerProfileIds(IReadOnlyList<Guid> memberIds)
        {
           if(memberIds == null || !memberIds.Any())
            {
                return Array.Empty<Guid>();
            }
            return await FindByCondition(sp => memberIds.Contains(sp.SellerId), false)
                .Select(sp => sp.SellerProfileId)
                .ToListAsync();
        }

        public async Task<string?> GetSellerWhatsAppNumber(Guid userProfileId)
        {
            return await FindByCondition(p => p.SellerId == userProfileId, false)
                .Select(p => p.WhatsAppNumber != null ? p.WhatsAppNumber : "")
                .FirstOrDefaultAsync();
        }

        public Task<int> NumberOfSellers() => FindAll(false).CountAsync();




        public async Task<long> NextSellerProfileSlugNumberAsync()
        {
            var connection = _context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            await using var command = connection.CreateCommand();

            command.CommandText = "SELECT NEXT VALUE FOR SellerProfileSlugSeq";

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt64(result);
        }





        public async Task<SellerShopDto?> GetSellerShopDetailsBySellerSlug(string slug)
        {
            return await FindByCondition(p => p.Slug == slug, false).Select(s => new SellerShopDto()
            {
                SellerProfileId = s.SellerProfileId,
                SellerId = s.SellerId,
                SellerName = s.SellerName,
                SellerSlugName = s.Slug,
                

                WhatsAppNumber = s.WhatsAppNumber,
                SellerTypeDescription = s.SellerType != null ? s.SellerType.SellerTypeName : "Not Categorized",
                Products = s.Products.Any() ? s.Products.Select(p => new SellerProductDto()
                {
                    ProductId = p.ProductId,
                    Price = p.Price,
                    ProductName = p.ProductName,
                    SlugName = p.Slug,
                    SellerSlugName = s.Slug,
                    CategoryName = p.Category != null ? p.Category.CategoryName : "Not Categorized",
                    ReviewSummary = p.ProductReviews.Count != 0 ? (int)p.ProductReviews.Average(r => r.Rating) : 0,
                    ProductImageId = p.Images.OrderByDescending(i => i.IsPrimary).Select(i => i.ProductImageId).FirstOrDefault(),
                }).ToList() : new List<SellerProductDto>(),
                Trades = s.Trades.Any() ? s.Trades.Select(p => new SellerTradeDto()
                {
                    TradeId = p.TradeId,
                    SellerSlugName = s.Slug,
                    TradeSlugName = p.Slug,
                    TradeName = p.TradeName,
                    Description = p.Description,
                    TradeImageId = p.Images.OrderByDescending(i => i.IsPrimary).Select(i => i.TradeImageId).FirstOrDefault(),
                    CategoryName = p.Category != null ? p.Category.CategoryName : "Not Categorized",
                    ReviewSummary = p.TradeReviews.Count != 0 ? (int)p.TradeReviews.Average(r => r.Rating) : 0,
                }).ToList() : new List<SellerTradeDto>(),
            }).FirstOrDefaultAsync();
        }


        public async Task<GroupProductsAndTradesList?> GetGroupProductsAndTradesListBySellerSlug(string slug)
        {

            return await FindByCondition(p => p.Slug == slug, false).Select(s => new GroupProductsAndTradesList()
            {

                GroupProducts = s.Products.Any() ? s.Products.Select(p => new SellerProductDto()
                {
                    ProductId = p.ProductId,
                    Price = p.Price,
                        SlugName = p.Slug,
                        SellerSlugName = s.Slug,
                    ProductName = p.ProductName,
                    CategoryName = p.Category != null ? p.Category.CategoryName : "Not Categorized",
                    ReviewSummary = p.ProductReviews.Count != 0 ? (int)p.ProductReviews.Average(r => r.Rating) : 0,
                    ProductImageId = p.Images.OrderByDescending(i => i.IsPrimary).Select(i => i.ProductImageId).FirstOrDefault(),
                }).ToList() : new List<SellerProductDto>(),
                GroupTrades = s.Trades.Any() ? s.Trades.Select(p => new SellerTradeDto()
                {
                    TradeId = p.TradeId,
                    SellerSlugName = s.Slug,
                    TradeSlugName = p.Slug,

                    TradeName = p.TradeName,
                    Description = p.Description,
                    TradeImageId = p.Images.OrderByDescending(i => i.IsPrimary).Select(i => i.TradeImageId).FirstOrDefault(),
                    CategoryName = p.Category != null ? p.Category.CategoryName : "Not Categorized",
                    ReviewSummary = p.TradeReviews.Count != 0 ? (int)p.TradeReviews.Average(r => r.Rating) : 0,
                }).ToList() : new List<SellerTradeDto>(),
            }).FirstOrDefaultAsync();
        }


        public async Task<SellerProfile?> FindSellerProfileBySlugName(string slugName, bool tracking)
        {
            return await FindByCondition(sp => sp.Slug == slugName, tracking)
                .FirstOrDefaultAsync();
        }
        public async Task<MiniGroupDetailsDto?> FindMiniGroupDetailsBySlugName(string slugName)
        {
            return await FindByCondition(p => p.Slug == slugName, false).Select(p => new MiniGroupDetailsDto()
            {
                SellerProfileId = p.SellerProfileId,
                SlugName = p.Slug,
                SellerId = p.SellerId,
                SellerName = p.SellerName,
                SellerTypeDescription = p.SellerType != null ? p.SellerType.SellerTypeName : "not categorized"

            }).FirstOrDefaultAsync();


        }


        public async Task<int> MakeSellerGroupSeller(Guid sellerProfileId)
        {
            var rowsAffected = await FindByCondition(p => p.SellerProfileId == sellerProfileId, true)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(
                        p => p.IsGroupSeller,  true
                    ));
            return rowsAffected;
        }

        public async Task<SellerProfile?> FindSellerProfileBySellerSlug(string slug)
        {
            return await FindByCondition(sp => sp.Slug == slug, false)
                .FirstOrDefaultAsync();
        }
        public Task<Guid> GetSellerProfileIdBySlugName(string slug) => FindByCondition(p => p.Slug == slug, false).Select(k => k.SellerProfileId).FirstOrDefaultAsync();












        public async Task<List<SlugInfo>> GetAllSellerSlugsAsync()
        {
            return await FindAll(false)
                .Select(sp => new SlugInfo
                {
                    UpdatedAt = DateTime.UtcNow,
                    Slug = sp.Slug
                })
                .ToListAsync();
        }







        }
}
