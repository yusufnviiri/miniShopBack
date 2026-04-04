using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IWishListService
    {

        Task<IEnumerable<ShowWishListDto>> GetAllWishListsAsync();
        Task<WishList?> FindWishListByIdAsync(Guid OrderId, bool tracking);
        Task<IEnumerable<ShowWishListDto>> FindWishListsByDateAsync(DateTime listdate);
        Task CreateWishListAsync(IEnumerable<WishListItem> wishListItems, Guid userId);
        Task UpdateWishListAsync(WishList wishList);
        Task DeleteWishListAsync(Guid wishListId);
    }
}
