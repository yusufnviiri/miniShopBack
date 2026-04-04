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
     internal sealed class BuyerProfileService : IBuyerProfileService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public BuyerProfileService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

       public async Task<IEnumerable<BuyerProfileDto>> GetAllBuyerProfilesAsync()=>await _repoManager.BuyerProfileRepo.GetAllBuyerProfiles();
        public async Task<BuyerProfile?> FindBuyerProfileByIdAsync(Guid buyerProfileId, bool tracking)=>
            await _repoManager.BuyerProfileRepo.FindBuyerProfileById(buyerProfileId, tracking);
        public async Task CreateBuyerProfileAsync(BuyerProfile buyerProfile)
        {
            _repoManager.BuyerProfileRepo.CreateBuyerProfile(buyerProfile);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateBuyerProfileAsync(BuyerProfileDto buyerProfileDto)
        {
            var existingBuyerProfile = await _repoManager.BuyerProfileRepo.FindBuyerProfileById(buyerProfileDto.BuyerProfileId, true);
            if (existingBuyerProfile is null)
            {
                _logger.LogError($"Buyer Profile with id: {buyerProfileDto.BuyerProfileId} not found.");
                throw new ObjectBadRequestExeption($"object with id {buyerProfileDto.BuyerProfileId} not found");
            }
        
            existingBuyerProfile.PurchaseLimit = buyerProfileDto.PurchaseLimit;
            existingBuyerProfile.BuyerTierId = buyerProfileDto.BuyerTierId;
            existingBuyerProfile.BuyerTypeId = buyerProfileDto.BuyerTypeId;
            _repoManager.BuyerProfileRepo.UpdateBuyerProfile(existingBuyerProfile);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteBuyerProfileAsync(Guid buyerProfileId)
        {
            var existingBuyerProfile = await _repoManager.BuyerProfileRepo.FindBuyerProfileById(buyerProfileId, tracking: true);
            if (existingBuyerProfile is null)
            {
                _logger.LogError($"Buyer Profile with id: {buyerProfileId} not found.");
                throw new ObjectBadRequestExeption($"object with id {buyerProfileId} not found");
            }
            _repoManager.BuyerProfileRepo.DeleteBuyerProfile(existingBuyerProfile);
            await _repoManager.SaveRepoDataAsync();
        }
    }
}
