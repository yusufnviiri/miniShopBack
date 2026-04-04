using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItemDto>> GetAllCartItemsAsync();
        Task<CartItem?> FindCartItemByIdAsync(Guid cartItemId, bool tracking);
        Task CreateCartItemAsync(CartItemDto cartItem);
        Task UpdateCartItemAsync(CartItemDto cartItem);
        Task DeleteCartItemAsync(Guid cartItemId );
    }
}
