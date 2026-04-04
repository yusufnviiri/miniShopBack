using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IShoppingCartRepo
    {
        Task<IEnumerable<ShoppingCartDto>> GetAllhoppingCarts();
        Task<ShoppingCart?> FindShoppingCartById(Guid cartId, bool tracking);
        void CreateShoppingCart(ShoppingCart cart);
        void UpdateShoppingCart(ShoppingCart cart);
        void DeleteShoppingCart(ShoppingCart cart);
    }
}
