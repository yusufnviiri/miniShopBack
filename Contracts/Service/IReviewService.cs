using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IReviewService
    {
        Task<IEnumerable<ShowReviewDto>> GetAllReviewsAsync();
        Task<IEnumerable<ShowReviewDto>> GetAllProductReviewsAsync(Guid productId);
        Task<Review?> FindReviewByIdAsync(Guid reviewId, bool tracking);
        Task CreateReviewAsync(NewReviewDto reviewDto);
        Task UpdateReviewAsync(NewReviewDto reviewDto);
        Task DeleteReviewAsync(Guid reviewId);
    }
}
