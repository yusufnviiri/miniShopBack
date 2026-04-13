using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ITradeReviewService
    {

        Task<IEnumerable<ShowReviewDto>> GetAllReviewsAsync();
        Task<IEnumerable<ShowReviewDto>> GetAllTradeReviewsAsync(Guid tradeId);
        Task<TradeReview?> FindTradeReviewByIdAsync(Guid reviewId, bool tracking);
        Task CreateTradeReviewAsync(NewReviewDto review);
        Task UpdateTradeReviewAsync(NewReviewDto review,bool IsSameUser);
        Task DeleteTradeReviewAsync(Guid reviewId);
        Task<bool> IsReviewedByUserAsync(Guid userId,Guid tradeId);
    }
}
