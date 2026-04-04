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
    internal sealed class CartItemService : ICartItemService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private User? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public CartItemService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CartItemDto>> GetAllCartItemsAsync() => await _repoManager.CartItemRepo.GetAllCartItems();
        public async Task<CartItem?> FindCartItemByIdAsync(Guid cartItemId, bool tracking) => await _repoManager.CartItemRepo.FindCartItemById(cartItemId, tracking);
        public async Task CreateCartItemAsync(CartItemDto cartItem)
        {
            if (cartItem == null)
            {
                throw new ObjectBadRequestExeption("Cart item data is null");
            }
            var cartItemEntity = _mapper.Map<CartItem>(cartItem);
            _repoManager.CartItemRepo.CreateCartItem(cartItemEntity);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateCartItemAsync(CartItemDto cartItem)
        {
            var existingCartItem = await _repoManager.CartItemRepo.FindCartItemById(cartItem.CartItemId, true);
            if (existingCartItem != null)
            {
                var updatedCartItem = _mapper.Map(cartItem, existingCartItem);
                _repoManager.CartItemRepo.UpdateCartItem(existingCartItem);
                await _repoManager.SaveRepoDataAsync();
            }
        }
        public async Task DeleteCartItemAsync(Guid cartItemId)
        {
            var existingCartItem = await _repoManager.CartItemRepo.FindCartItemById(cartItemId, true);
            if (existingCartItem != null)
            {
                _repoManager.CartItemRepo.DeleteCartItem(existingCartItem);
                await _repoManager.SaveRepoDataAsync();
            }

        }
    }
}
