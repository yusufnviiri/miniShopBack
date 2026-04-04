using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ICartItemRepo
    {
        Task<IEnumerable<CartItemDto>> GetAllCartItems();
        Task<CartItem?> FindCartItemById(Guid cartItemId, bool tracking);
        void CreateCartItem(CartItem cartItem );
        void UpdateCartItem(CartItem cartItem);
        void DeleteCartItem(CartItem cartItem);
    }
}
