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
    
        public class ReviewRepo : RepositoryBase<Review>, IReviewRepo
        {
            public ReviewRepo(ApplicationDbContext _db) : base(_db)
            {

            }

        public async Task<IEnumerable<ShowReviewDto>> GetAllReviews()
        {
            return await FindAll(false).Select(r=> new ShowReviewDto
            {
                ReviewId = r.ReviewId,
                ApplicationUserId = r.ApplicationUserId,
                ReviewerName = $"{r.Reviewer.FirstName}" +" "+ $"{r.Reviewer.LastName}",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToListAsync();

        }
        public async Task<IEnumerable<ShowReviewDto>> GetAllProductReviews(Guid productId)
        {
            return await FindByCondition(r=>r.ProductId==productId,false).Select(r => new ShowReviewDto
            {
                ReviewId = r.ReviewId,
                ApplicationUserId = r.ApplicationUserId,
                ReviewerName = $"{r.Reviewer.FirstName}" + " " + $"{r.Reviewer.LastName}",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToListAsync();
        }
        public async Task<Review?> FindReviewById(Guid reviewId, bool tracking)=>await FindByCondition(r=>r.ReviewId==reviewId,tracking).FirstOrDefaultAsync();
        public void CreateReview(Review review)=>CreateBase(review);
        public void UpdateReview(Review review)=>UpdateBase(review);
       public  void DeleteReview(Review review)=>DeleteBase(review);
    }
    }