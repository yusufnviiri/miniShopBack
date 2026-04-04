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
     internal sealed class SellerRestrictionService : ISellerRestrictionService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public SellerRestrictionService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

      public async Task<IEnumerable<SellerRestrictionDto>> GetAllSellerRestrictionsAsync()=>await _repoManager.SellerRestrictionRepo.GetAllSellerRestrictions();
       public async Task<SellerRestriction?> FindSellerRestrictionByIdAsync(Guid restrictionId, bool tracking)=>await _repoManager.SellerRestrictionRepo.FindSellerRestrictionById(restrictionId, tracking);
       public async Task CreateSellerRestrictionAsync(SellerRestriction restriction)
        {
            _repoManager.SellerRestrictionRepo.CreateSellerRestriction(restriction);
            await _repoManager.SaveRepoDataAsync();
        }
      public async  Task UpdateSellerRestrictionAsync(SellerRestrictionDto restriction)
        {
   var existingRestriction= await _repoManager.SellerRestrictionRepo.FindSellerRestrictionById(restriction.SellerRestrictionId, true);
            if (existingRestriction == null)
            {
                throw new ObjectBadRequestExeption("Seller restriction not found.");
            }
            existingRestriction.ExpiresAt = restriction.ExpiresAt;
            existingRestriction.Reason = restriction.Reason;


            _repoManager.SellerRestrictionRepo.UpdateSellerRestriction(existingRestriction);
            await _repoManager.SaveRepoDataAsync();
        }
      public async  Task DeleteSellerRestrictionAsync(Guid restrictionId)
        {
            var existingRestriction = await _repoManager.SellerRestrictionRepo.FindSellerRestrictionById(restrictionId, tracking: true);
            if (existingRestriction == null)
            {
                _logger.LogError($"Seller restriction with id: {restrictionId} not found.");
                
                throw new ObjectBadRequestExeption($"object with id {restrictionId} not found");
            }
            _repoManager.SellerRestrictionRepo.DeleteSellerRestriction(existingRestriction);
            await _repoManager.SaveRepoDataAsync();
        }

    }
}
