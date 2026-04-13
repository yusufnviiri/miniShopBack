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

    public class ProductReviewRepo : RepositoryBase<ProductReview>, IProductReviewRepo
    {
        public ProductReviewRepo(ApplicationDbContext _db) : base(_db)
        {

        }

        public async Task<IEnumerable<ShowReviewDto>> GetAllReviews()
        {
            return await FindAll(false).Select(r => new ShowReviewDto
            {
                ReviewId = r.ProductReviewId,
                UserProfileId = r.UserProfileId,
                ReviewerName = $"{r.Reviewer.IdentityUser.FirstName}" + " " + $"{r.Reviewer.IdentityUser.LastName}",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToListAsync();

        }
        public async Task<IEnumerable<ShowReviewDto>> GetAllProductReviews(Guid productId)
        {
            return await FindByCondition(r => r.ProductReviewId == productId, false).Select(r => new ShowReviewDto
            {
                ReviewId = r.ProductReviewId,
                UserProfileId = r.UserProfileId,
                ReviewerName = $"{r.Reviewer.IdentityUser.FirstName}" + " " + $"{r.Reviewer.IdentityUser.LastName}",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToListAsync();
        }
        public async Task<ProductReview?> FindProductReviewById(Guid reviewId, bool tracking) => await FindByCondition(r => r.ProductReviewId == reviewId, tracking).FirstOrDefaultAsync();
        public void CreateProductReview(ProductReview review) => CreateBase(review);
        public void UpdateProductReview(ProductReview review) => UpdateBase(review);
        public void DeleteProductReview(ProductReview review) => DeleteBase(review);
        public IQueryable<ProductReview> ProductReviewsQuery()=>FindAll(false);
        public async Task<bool> IsReviewedByUser(Guid userId, Guid productId)
        {
            var isReviewed = await FindByCondition(p=>p.UserProfileId == userId&&p.ProductId==productId, false).FirstOrDefaultAsync();
            if (isReviewed == null) return false; return true;
        }

       
        public async Task<ProductReview?> FindProductReviewByProductIdAndUserId(Guid productId, Guid userId, bool tracking) => await FindByCondition(r => r.ProductId == productId && r.UserProfileId == userId, tracking).FirstOrDefaultAsync();


    }
} 