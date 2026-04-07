using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
   public interface IRepositoryManager
    {
      
        Task SaveRepoDataAsync();
        public IUserGroupRepo UserGroupRepo { get; }
        public IApplicationUserRepo UserRepo { get; }
        public IProductImageRepo ProductImageRepo { get; }
        public IProductRepo ProductRepo { get; }
        public IOrderItemRepo OrderItemRepo { get; }
        public IOrderRepo OrderRepo { get; }
        public ICartItemRepo CartItemRepo { get; }
        public IShoppingCartRepo ShoppingCartRepo { get; }
        public IReviewRepo ReviewRepo { get; }
        public IWishListRepo WishListRepo { get; }
        public IWishListItemRepo WishListItemRepo { get; }
        public IPaymentRepo PaymentRepo { get; }
        public ISubCategoryCategoryRepo SubCategoryCategoryRepo { get; }
        public ISubCategoryRepo SubCategoryRepo { get; }
        public ICategoryRepo CategoryRepo { get; }
        public IGroupMemberRepo GroupMemberRepo { get; }
        public IUserProfileRepo UserProfileRepo { get; }
        public IAddressRepo AddressRepo { get; }
        public IApplicationUserRepo ApplicationUserRepo { get; }
        public ISellerRestrictionRepo SellerRestrictionRepo { get; }
        public IBuyerProfileRepo BuyerProfileRepo { get; }
        public ISellerProfileRepo SellerProfileRepo { get; }
        public ISellerViolationRepo SellerViolationRepo { get; }
        public IUserGroupSellerRepo UserGroupSellerRepo { get; }
        public IUserGroupNotificationRepo UserGroupNotificationRepo { get; }
        public ICategoryAttributeRepo CategoryAttributeRepo { get; }
        public IProductAttributeValueRepo ProductAttributeValueRepo { get; }
        public IUserOtpRepo UserOtpRepo { get; }
        public IUserDeviceRepo UserDeviceRepo { get; }
        public IRefreshTokenRepo RefreshTokenRepo { get; }
        public IGeneralCategoryRepo GeneralCategoryRepo {get; }
        public ITradeRepo TradeRepo { get; }
        public ITradeAttributeValueRepo TradeAttributeValueRepo { get; }
        public ITradeImageRepo TradeImageRepo { get; }
        public IGroupSellerRepo GroupSellerRepo { get; }
        public IHomePageCardRepo HomePageCardRepo { get; }

    }
}
