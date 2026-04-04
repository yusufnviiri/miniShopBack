using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public sealed class GroupSellerService : IGroupSellerService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper; 

        public GroupSellerService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
      
        }



        public async  Task<IEnumerable<GroupSeller>> GetAllGroupSellersAsync()=>await _repoManager.GroupSellerRepo.GetAllGroupSellers();
        public async Task<GroupSeller?> FindGroupSellerByIdAsync(Guid groupSellerId, bool tracking)=>await _repoManager.GroupSellerRepo.FindGroupSellerById(groupSellerId, tracking);
        public async Task CreateGroupSellerAsync(GroupSeller groupSeller)=> await _repoManager.GroupSellerRepo.GetAllGroupSellers();
        public async Task UpdateGroupSellerAsync(GroupSeller groupSeller)=> await _repoManager.GroupSellerRepo.GetAllGroupSellers();
        public async Task DeleteGroupSellerAsync(Guid groupSellerId)
        {
            var groupSeller = await _repoManager.GroupSellerRepo.FindGroupSellerById(groupSellerId, true);
            if (groupSeller != null)
            {
                _repoManager.GroupSellerRepo.DeleteGroupSeller(groupSeller);
                await _repoManager.SaveRepoDataAsync();
            }

        }

    }
}
