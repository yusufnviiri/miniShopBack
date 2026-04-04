using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IReviewRepo
    {
        Task<IEnumerable<ShowReviewDto>> GetAllReviews();
        Task<IEnumerable<ShowReviewDto>> GetAllProductReviews(Guid productId);
        Task<Review?> FindReviewById(Guid reviewId, bool tracking);
        void CreateReview(Review review );
        void UpdateReview(Review review);
        void DeleteReview(Review review);
    }
}
