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
    public class WishListRepo : RepositoryBase<WishList>, IWishListRepo
    {
        public WishListRepo(ApplicationDbContext _db) : base(_db)
        {}
      public async  Task<IEnumerable<ShowWishListDto>> GetAllWishLists()
        {
            return await FindAll(false).Select(p => new ShowWishListDto() { WishListId = p.WishListId, UserId = p.UserId, SelectedItems = p.WishListItems.Select(k => new ShowProductMiniDetailsDto() { ProductId = k.ProductId, ProductName = k.Product.ProductName, Price = k.Product.Price, ProductImageRefs = k.Product.Images.Select(i => new ProductImageRefDto() { ProductImageId = i.ProductImageId, IsPrimary = i.IsPrimary }).ToList() }).ToList() }).ToListAsync();
        }
        public  Task<WishList?> FindWishListById(Guid wishListId, bool tracking)
        {
            return FindByCondition(wl=>wl.WishListId==wishListId,tracking).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ShowWishListDto>> FindWishListsByDate(DateTime listdate)
        {
            var start = listdate.Date;
            var end = start.AddDays(1);
            return await FindByCondition(ld=>ld.CreatedAt>=start && ld.CreatedAt<end,false).Select(p=>new ShowWishListDto() { WishListId = p.WishListId, UserId = p.UserId, SelectedItems = p.WishListItems.Select(k => new ShowProductMiniDetailsDto() {ProductId=k.ProductId,ProductName=k.Product.ProductName,Price=k.Product.Price,ProductImageRefs=k.Product.Images.Select (i=>new ProductImageRefDto() { ProductImageId=i.ProductImageId,IsPrimary=i.IsPrimary}).ToList() }).ToList() }).ToListAsync();
        }
       

      
        public void CreateWishList(WishList wishList)=> CreateBase(wishList);
        public void UpdateWishList(WishList wishList)=> UpdateBase(wishList);
        public void DeleteWishList(WishList wishList) => DeleteBase(wishList);
    }
}