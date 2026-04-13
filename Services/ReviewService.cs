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
    internal sealed class ReviewService:IReviewService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

       public async Task<IEnumerable<ShowReviewDto>> GetAllReviewsAsync()=>await _repoManager.ReviewRepo.GetAllReviews();
        public async Task<IEnumerable<ShowReviewDto>> GetAllProductReviewsAsync(Guid productId)=> await _repoManager.ReviewRepo.GetAllProductReviews(productId);
        public async Task<Review?> FindReviewByIdAsync(Guid reviewId, bool tracking)=>await _repoManager.ReviewRepo.FindReviewById(reviewId,true);
        public async Task CreateReviewAsync(NewReviewDto reviewDto)
        {
            if (reviewDto == null)
            {
               throw new ObjectBadRequestExeption("Review data is null");
            }
            var reviewEntity = _mapper.Map<Review>(reviewDto);
            _repoManager.ReviewRepo.CreateReview(reviewEntity);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateReviewAsync(NewReviewDto reviewDto)
        {
            var existingReview = await _repoManager.ReviewRepo.FindReviewById((Guid)reviewDto.ReviewId, true);
            if (reviewDto == null)
            {
                throw new ObjectBadRequestExeption("Review data is null");
            }
            existingReview.Rating = reviewDto.Rating;
            existingReview.Comment = reviewDto.Comment;

            _repoManager.ReviewRepo.UpdateReview(existingReview);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteReviewAsync(Guid reviewId)
        {
             var existingReview = await _repoManager.ReviewRepo.FindReviewById(reviewId, true);
            if (existingReview == null)
            {
                throw new ItemNotFoundException(reviewId);
            }
            _repoManager.ReviewRepo.DeleteReview(existingReview);
            await _repoManager.SaveRepoDataAsync();
        }
        
    }
}
