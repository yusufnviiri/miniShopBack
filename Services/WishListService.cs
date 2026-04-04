using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class WishListService:IWishListService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishListService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ShowWishListDto>> GetAllWishListsAsync() => await _repoManager.WishListRepo.GetAllWishLists();
        public  Task<WishList?> FindWishListByIdAsync(Guid OrderId, bool tracking)=>_repoManager.WishListRepo.FindWishListById(OrderId, tracking);
        public async Task<IEnumerable<ShowWishListDto>> FindWishListsByDateAsync(DateTime listdate)=> await _repoManager.WishListRepo.GetAllWishLists();
        public async Task CreateWishListAsync(IEnumerable<WishListItem> wishListItems,Guid userId)
        {

            WishList wishList = new WishList()
            {
                UserId = userId,
                WishListItems = wishListItems.ToList()

            };
            _repoManager.WishListRepo.CreateWishList(wishList);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateWishListAsync(WishList wishList)
        {
            var existingList =await _repoManager.WishListRepo.FindWishListById(wishList.WishListId, true);
            _mapper.Map(existingList, wishList);
            _repoManager.WishListRepo.UpdateWishList(wishList);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteWishListAsync(Guid wishListId)
        {
            var exixtingList = await _repoManager.WishListRepo.FindWishListById(wishListId, true);
            if (exixtingList != null) {
                _repoManager.WishListRepo.DeleteWishList(exixtingList);
                await _repoManager.SaveRepoDataAsync();

        }
    }
}
    }