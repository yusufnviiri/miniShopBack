using Entities.Models;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IProductRepo
    {
        Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProducts(bool tracking);
        //void MakeProductFeautured(Guid productId);
        Task MakeAllProductsFeatured();
        Task<HomePageCustomProductsDto?> HomePageCustomProducts ();

        Task<PagedList<HomePageProductDto>> GetHomePageProducts(ProductRequestParameters requestParameters);


        Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProductsByCategory(bool tracking,string categoryName);
        Task<ShowProductDto?> FindProductById(bool tracking, Guid productId);
        Task<ShowProductDto?> FindProductBySlugName(bool tracking, string slugName);

        Task<ProductDataDto?> GetProductData( Guid productId);
        Task<ProductDataDto?> GetProductDataUsingSlugName(string slugName);
        Task<ShowProductDto?> FindSellerProduct( Guid productId);
        Task<ShowProductDto?> FindSellerProductUsingSlugName(bool tracking, string slugName);
        Task<Product?> FindProductForUpdate( Guid productId);
        Guid CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product productId);
        Task<int> NumberOfProducts();
        Task<Guid> GetProductIdBySlugName(string slug);

        Task<ICollection<SellerProductsListDto>> GetGroupMembersProducts(IList<Guid> groupMemberIds);
        Task<ICollection<SellerProductsListDto>> GetGroupMembersForDisplayProducts(IList<Guid> groupMemberIds, Guid groupId);

        //Task <IEnumerable<ShowProductMiniDetailsDto>> GetAllSellerProducts(Guid sellerProfileId);
        // Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllStockedSellerProducts(Guid sellerProfileId);


    }
}
