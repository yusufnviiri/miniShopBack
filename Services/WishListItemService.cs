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
    internal sealed class WishListItemService : IWishListItemService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishListItemService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

       public async Task<IEnumerable<WishListItemDto>> GetAllWishListItemsAsync()=>
            await _repoManager.WishListItemRepo.GetAllWishListItems();
        public  Task<WishListItem?> FindWishListItemByIdAsync(Guid WishListItemId, bool tracking)=>
             _repoManager.WishListItemRepo.FindWishListItemById(WishListItemId, tracking);
        public async Task CreateWishListItemAsync(WishListItemDto listItem)
        {
            var listItemEntity = _mapper.Map<WishListItem>(listItem);
            _repoManager.WishListItemRepo.CreateWishListItem(listItemEntity);
            await _repoManager.SaveRepoDataAsync();

        }
        public async Task UpdateWishListItemAsync(WishListItemDto listItem)
        {
            var existingListItem = await _repoManager.WishListItemRepo.FindWishListItemById(listItem.WishListItemId, true);
            if (existingListItem == null)
            {
                throw new ItemNotFoundException(listItem.WishListItemId);
            }
            _mapper.Map(listItem, existingListItem);
            _repoManager.WishListItemRepo.UpdateWishListItem(existingListItem);
            await _repoManager.SaveRepoDataAsync();

        }
        public async Task DeleteWishListItemAsync(Guid listItemId)
        {
            var existingListItem = await _repoManager.WishListItemRepo.FindWishListItemById(listItemId, true);
            if (existingListItem == null)
            {
                throw new ItemNotFoundException(listItemId);
            }
            _repoManager.WishListItemRepo.DeleteWishListItem(existingListItem);
            await _repoManager.SaveRepoDataAsync();

        }


    }
}