using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class TradeReviewRepo : RepositoryBase<TradeReview>, ITradeReviewRepo
    {
        public TradeReviewRepo(ApplicationDbContext _db) : base(_db)
        {

        }

        public async Task<IEnumerable<ShowReviewDto>> GetAllReviews()
        {
            return await FindAll(false).Select(r => new ShowReviewDto
            {
                ReviewId = r.TradeReviewId,
                UserProfileId = r.UserProfileId,
                ReviewerName = $"{r.Reviewer.IdentityUser.FirstName}" + " " + $"{r.Reviewer.IdentityUser.LastName}",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToListAsync();

        }
        public async Task<IEnumerable<ShowReviewDto>> GetAllTradeReviews(Guid tradeId)
        {
            return await FindByCondition(r => r.TradeReviewId == tradeId, false).Select(r => new ShowReviewDto
            {
                ReviewId = r.TradeReviewId,
                UserProfileId = r.UserProfileId,
                ReviewerName = $"{r.Reviewer.IdentityUser.FirstName}" + " " + $"{r.Reviewer.IdentityUser.LastName}",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToListAsync();
        }
        public async Task<TradeReview?> FindTradeReviewById(Guid reviewId, bool tracking) => await FindByCondition(r => r.TradeReviewId == reviewId, tracking).FirstOrDefaultAsync();
        public void CreateTradeReview(TradeReview review) => CreateBase(review);
        public void UpdateTradeReview(TradeReview review) => UpdateBase(review);
        public void DeleteTradeReview(TradeReview review) => DeleteBase(review);
        public IQueryable<TradeReview> TradeReviewsQuery()=> FindAll(false);
        public async Task<bool> IsReviewedByUser(Guid userId, Guid tradeId)
        {
            var isReviewed = await FindByCondition(p => p.UserProfileId == userId && p.TradeId == tradeId, false).FirstOrDefaultAsync();
            if (isReviewed == null) return false; return true;
        }

     

        public async Task<TradeReview?> FindTradeReviewByTradeIdAndUserId(Guid tradeId, Guid userId, bool tracking) => await FindByCondition(r => r.TradeId == tradeId && r.UserProfileId == userId, tracking).FirstOrDefaultAsync();

    }

}

