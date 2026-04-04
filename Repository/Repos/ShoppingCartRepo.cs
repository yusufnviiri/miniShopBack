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
   public class ShoppingCartRepo : RepositoryBase<ShoppingCart>, IShoppingCartRepo
    {
        public ShoppingCartRepo(ApplicationDbContext _db) : base(_db)
        {

        }
        public async Task<IEnumerable<ShoppingCartDto>> GetAllhoppingCarts()
        {
            return await FindAll(false).Select(c => new ShoppingCartDto
            {
                ShoppingCartId = c.ShoppingCartId,
                ApplicationUserId = c.ApplicationUserId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                Items = c.Items.Select(i => new CartItem
                {
                    CartItemId = i.CartItemId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                   
                }).ToList()
            }).ToListAsync();

        }
        public async Task<ShoppingCart?> FindShoppingCartById(Guid cartId, bool tracking)=> await FindByCondition(p=>p.ShoppingCartId==cartId,tracking).FirstOrDefaultAsync();
        public void CreateShoppingCart(ShoppingCart cart)=>CreateBase(cart);
        public void UpdateShoppingCart(ShoppingCart cart)=>UpdateBase(cart);
        public void DeleteShoppingCart(ShoppingCart cart) => DeleteBase(cart);
    }
}