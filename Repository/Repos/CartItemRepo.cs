using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class CartItemRepo : RepositoryBase<CartItem>, ICartItemRepo
    {
        public CartItemRepo(ApplicationDbContext _db) : base(_db)
        {

        }
        public async Task<IEnumerable<CartItemDto>> GetAllCartItems() {
             return await FindAll(false).Select(c => new CartItemDto
            {
                CartItemId = c.CartItemId,
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                UnitPrice = c.UnitPrice,
                ProductName = c.ProductName,
            }).ToListAsync();
        }   
 
        public async Task<CartItem?> FindCartItemById(Guid cartItemId, bool tracking)=>await FindByCondition(p=>p.CartItemId==cartItemId,tracking).FirstOrDefaultAsync();
        public void CreateCartItem(CartItem cartItem)=> CreateBase(cartItem);
        public void UpdateCartItem(CartItem cartItem)=>UpdateBase(cartItem);
        public void DeleteCartItem(CartItem cartItem)=>DeleteBase(cartItem);
    }
}
