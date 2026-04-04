using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IWishListRepo
    {
        Task<IEnumerable<ShowWishListDto>> GetAllWishLists();
        Task<WishList?> FindWishListById(Guid OrderId, bool tracking);
        Task<IEnumerable<ShowWishListDto>> FindWishListsByDate(DateTime listdate);
        void CreateWishList(WishList wishList);
        void UpdateWishList(WishList wishList);
        void DeleteWishList(WishList wishList);

    }
}
