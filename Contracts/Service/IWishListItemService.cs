using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IWishListItemService
    {
        Task<IEnumerable<WishListItemDto>> GetAllWishListItemsAsync();
        Task<WishListItem?> FindWishListItemByIdAsync(Guid WishListItemId, bool tracking);
        Task CreateWishListItemAsync(WishListItemDto listItem);
        Task UpdateWishListItemAsync(WishListItemDto listItem);
        Task DeleteWishListItemAsync(Guid listItemId);
    }
}
