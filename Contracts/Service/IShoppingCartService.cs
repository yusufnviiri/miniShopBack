using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartDto>> GetAllhoppingCartsAsync();
        Task<ShoppingCart?> FindShoppingCartByIdAsync(Guid cartId, bool tracking);
        Task CreateShoppingCartAsync(ShoppingCart cart);
        Task UpdateShoppingCartAsync(ShoppingCart cart);
        Task DeleteShoppingCartAsync(Guid cartId);
    }
}
