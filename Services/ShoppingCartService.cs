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
    internal sealed class ShoppingCartService:IShoppingCartService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }
      public async  Task<IEnumerable<ShoppingCartDto>> GetAllhoppingCartsAsync()=>await _repoManager.ShoppingCartRepo.GetAllhoppingCarts();
        public async Task<ShoppingCart?> FindShoppingCartByIdAsync(Guid cartId, bool tracking)=>await _repoManager.ShoppingCartRepo.FindShoppingCartById(cartId,tracking) ;
        public async Task CreateShoppingCartAsync(ShoppingCart cart)
        {
            if (cart == null)
            {
                throw new ObjectBadRequestExeption( "Shopping cart data is null");
            }
            _repoManager.ShoppingCartRepo.CreateShoppingCart(cart);
            await _repoManager.SaveRepoDataAsync();

        }
        public async Task UpdateShoppingCartAsync(ShoppingCart cart)
        {
            var existingCart = await _repoManager.ShoppingCartRepo.FindShoppingCartById(cart.ShoppingCartId, true);
            if (cart == null)
            {
                throw new ObjectBadRequestExeption("Shopping cart data is null");
            }
            var mappedCart = _mapper.Map(cart, existingCart);
            _repoManager.ShoppingCartRepo.UpdateShoppingCart(mappedCart);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteShoppingCartAsync(Guid cartId)
        {
            var existingCart = await _repoManager.ShoppingCartRepo.FindShoppingCartById(cartId, true);
            if (existingCart == null)
            {
                throw new ObjectBadRequestExeption("Shopping cart not found");
            }
            _repoManager.ShoppingCartRepo.DeleteShoppingCart(existingCart);
            await _repoManager.SaveRepoDataAsync();
        }
    }
}
