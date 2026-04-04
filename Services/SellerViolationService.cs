using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
     internal sealed class SellerViolationService : ISellerViolationService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public SellerViolationService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

     public async   Task<IEnumerable<SellerViolation>> GetAllSellerViolations()=>await _repoManager.SellerViolationRepo.GetAllSellerViolations();
        public async Task<SellerViolation?> FindSellerViolationById(Guid violationId, bool tracking)=>await _repoManager.SellerViolationRepo.FindSellerViolationById(violationId,tracking);
        public async Task CreateSellerViolation(SellerViolation violation)
        {
            _repoManager.SellerViolationRepo.CreateSellerViolation(violation);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateSellerViolation(SellerViolation violation)
        {
            var existingViolation = await _repoManager.SellerViolationRepo.FindSellerViolationById(violation.SellerViolationId, true);
            if (existingViolation == null)
            {
                _logger.LogError($"Seller Violation with id: {violation.SellerViolationId} not found.");
                throw new ObjectBadRequestExeption("Seller Violation not found.");
            }
            existingViolation.SellerProfileId = violation.SellerProfileId;
        }
        public async Task DeleteSellerViolation(Guid violationId)
        {
            var violationEntity = await _repoManager.SellerViolationRepo.FindSellerViolationById(violationId, tracking: true);
            if (violationEntity == null)
            {
                _logger.LogError($"Seller Violation with id: {violationId} not found.");
                throw new ObjectBadRequestExeption("Seller Violation not found.");
            }
            _repoManager.SellerViolationRepo.DeleteSellerViolation(violationEntity);
            await _repoManager.SaveRepoDataAsync();
        }

    }
}
