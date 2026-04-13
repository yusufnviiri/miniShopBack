using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IProductReviewRepo
    {

        Task<IEnumerable<ShowReviewDto>> GetAllReviews();
        IQueryable<ProductReview> ProductReviewsQuery();
        Task<bool> IsReviewedByUser(Guid userId, Guid productId);

        Task<IEnumerable<ShowReviewDto>> GetAllProductReviews(Guid productId);
        Task<ProductReview?> FindProductReviewById(Guid reviewId, bool tracking);
        Task<ProductReview?> FindProductReviewByProductIdAndUserId(Guid productId, Guid userId, bool tracking);

        void CreateProductReview(ProductReview review);
        void UpdateProductReview(ProductReview review);
        void DeleteProductReview(ProductReview review);
    }
}
