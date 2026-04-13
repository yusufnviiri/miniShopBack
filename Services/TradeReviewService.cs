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

    internal sealed class TradeReviewService : ITradeReviewService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public TradeReviewService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShowReviewDto>> GetAllReviewsAsync() => await _repoManager.TradeReviewRepo.GetAllReviews();
        public async Task<IEnumerable<ShowReviewDto>> GetAllTradeReviewsAsync(Guid tradeId) => await _repoManager.TradeReviewRepo.GetAllTradeReviews(tradeId);
        public async Task<TradeReview?> FindTradeReviewByIdAsync(Guid reviewId, bool tracking) => await _repoManager.TradeReviewRepo.FindTradeReviewById(reviewId, true);
        public async Task CreateTradeReviewAsync(NewReviewDto reviewDto)
        {



            TradeReview tradeReview = new()
            {
                UserProfileId = reviewDto.UserProfileId,
                Comment = reviewDto.Comment,
                Rating = reviewDto.Rating,
                TradeId = reviewDto.RefId,

            };

            _repoManager.TradeReviewRepo.CreateTradeReview(tradeReview);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateTradeReviewAsync(NewReviewDto reviewDto, bool IsSameUser)
        {
            TradeReview existingReview = new();
            if (!IsSameUser)
            {
                existingReview = await _repoManager.TradeReviewRepo.FindTradeReviewById(reviewDto.RefId, true);

            }
            else
            {
                existingReview = await _repoManager.TradeReviewRepo.FindTradeReviewByTradeIdAndUserId(reviewDto.RefId, reviewDto.UserProfileId, true);
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

                _repoManager.TradeReviewRepo.UpdateTradeReview(existingReview);
                await _repoManager.SaveRepoDataAsync();
            }
        }
        public async Task DeleteTradeReviewAsync(Guid reviewId)
        {
            var existingReview = await _repoManager.TradeReviewRepo.FindTradeReviewById(reviewId, true);
            if (existingReview == null)
            {
                throw new ItemNotFoundException(reviewId);
            }
            _repoManager.TradeReviewRepo.DeleteTradeReview(existingReview);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task<bool> IsReviewedByUserAsync(Guid userId, Guid tradeId)=>await _repoManager.TradeReviewRepo.IsReviewedByUser(userId,tradeId) ;


    }
}




