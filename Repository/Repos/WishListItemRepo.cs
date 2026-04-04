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
    public class WishListItemRepo: RepositoryBase<WishListItem>, IWishListItemRepo
    {
        public WishListItemRepo(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
        public async Task<IEnumerable<WishListItemDto>> GetAllWishListItems()
        {
            return await FindAll(false)
                .Select(wli => new WishListItemDto
                {
                    WishListItemId = wli.WishListItemId,
                    WishListId = wli.WishListId,
                    ProductId = wli.ProductId,
                    AddedAt = wli.AddedAt,
                    Note = wli.Note
                }).ToListAsync();
        }
        public  Task<WishListItem?> FindWishListItemById(Guid WishListItemId, bool tracking)
        {
            return FindByCondition(wli => wli.WishListItemId.Equals(WishListItemId), tracking)
                .FirstOrDefaultAsync();
        }
        public void CreateWishListItem(WishListItem listItem)=>
            CreateBase(listItem);
        public void UpdateWishListItem(WishListItem listItem)=>
            UpdateBase(listItem);
        public void DeleteWishListItem(WishListItem listItem)=>
            DeleteBase(listItem);

    }
}
