using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
 
    internal sealed class ProductReviewService : IProductReviewService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductReviewService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShowReviewDto>> GetAllReviewsAsync() => await _repoManager.ProductReviewRepo.GetAllReviews();
        public async Task<IEnumerable<ShowReviewDto>> GetAllProductReviewsAsync(Guid productId) => await _repoManager.ProductReviewRepo.GetAllProductReviews(productId);
        public async Task<ProductReview?> FindProductReviewByIdAsync(Guid reviewId, bool tracking) => await _repoManager.ProductReviewRepo.FindProductReviewById(reviewId, true);
        public async Task CreateProductReviewAsync(NewReviewDto reviewDto)
        {



            ProductReview productReview  = new()
            {
                UserProfileId = reviewDto.UserProfileId,
                Comment = reviewDto.Comment,
                Rating = reviewDto.Rating,
                ProductId = reviewDto.RefId,

            };

            _repoManager.ProductReviewRepo.CreateProductReview(productReview);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateProductReviewAsync(NewReviewDto reviewDto, bool IsSameUser)
        {
            ProductReview existingReview = new();
            if (!IsSameUser)
            {
                 existingReview = await _repoManager.ProductReviewRepo.FindProductReviewById(reviewDto.RefId, true);

            }else
            {
                 existingReview = await _repoManager.ProductReviewRepo.FindProductReviewByProductIdAndUserId(reviewDto.RefId, reviewDto.UserProfileId, true);
            }
            if (reviewDto == null)
            {
                throw new ObjectBadRequestExeption("Review data is null");
            }
            if (existingReview != null)
            {
                //existingReview.Rating = reviewDto.Rating;
                existingReview.Comment = reviewDto.Comment;
                existingReview.Rating = reviewDto.Rating;

                _repoManager.ProductReviewRepo.UpdateProductReview(existingReview);
                await _repoManager.SaveRepoDataAsync();
            }
        }
        public async Task DeleteProductReviewAsync(Guid reviewId)
        {
            var existingReview = await _repoManager.ProductReviewRepo.FindProductReviewById(reviewId, true);
            if (existingReview == null)
            {
                throw new ItemNotFoundException(reviewId);
            }
            _repoManager.ProductReviewRepo.DeleteProductReview(existingReview);
            await _repoManager.SaveRepoDataAsync();
        }

 
        public async Task<bool> IsReviewedByUserAsync(Guid userId, Guid productId)=> await _repoManager.ProductReviewRepo.IsReviewedByUser(userId, productId);

    }
}