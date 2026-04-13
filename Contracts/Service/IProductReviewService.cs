using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IProductReviewService
    {
        Task<IEnumerable<ShowReviewDto>> GetAllReviewsAsync();
        Task<IEnumerable<ShowReviewDto>> GetAllProductReviewsAsync(Guid productId);
        Task<ProductReview?> FindProductReviewByIdAsync(Guid reviewId, bool tracking);
        Task CreateProductReviewAsync(NewReviewDto review);
        Task UpdateProductReviewAsync(NewReviewDto review, bool IsSameUser);
        Task DeleteProductReviewAsync(Guid reviewId);
        Task<bool> IsReviewedByUserAsync(Guid userId, Guid productId);

    }
}
