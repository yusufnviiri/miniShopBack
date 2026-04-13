using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ITradeReviewRepo
    {

        Task<IEnumerable<ShowReviewDto>> GetAllReviews();
        IQueryable<TradeReview> TradeReviewsQuery();
        Task<IEnumerable<ShowReviewDto>> GetAllTradeReviews(Guid tradeId);
        Task<TradeReview?> FindTradeReviewById(Guid reviewId, bool tracking);
        void CreateTradeReview(TradeReview review);
        void UpdateTradeReview(TradeReview review);
        void DeleteTradeReview(TradeReview review);
        Task<bool> IsReviewedByUser(Guid userId, Guid tradeId);
        Task<TradeReview?> FindTradeReviewByTradeIdAndUserId(Guid tradeId, Guid userId, bool tracking);

    }
}
