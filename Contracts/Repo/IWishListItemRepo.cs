using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IWishListItemRepo
    {
        Task<IEnumerable<WishListItemDto>> GetAllWishListItems();
        Task<WishListItem?> FindWishListItemById(Guid WishListItemId, bool tracking);
        void CreateWishListItem(WishListItem listItem);
        void UpdateWishListItem(WishListItem listItem);
        void DeleteWishListItem(WishListItem listItem);
    }
}
