using Contracts.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IServiceManager
    {
        public IUserGroupService UserGroupService { get; }
        public IApplicationUserService UserService { get; }
        public IProductImageService ProductImageService { get; }
        public IProductService ProductService { get; }
        public IOrderItemService OrderItemService { get; }
        public IOrderService OrderService { get; }
        public ICartItemService CartItemService { get; }
        public IShoppingCartService ShoppingCartService { get; }
        public IReviewService ReviewService { get; }
        public IWishListService WishListService { get; }
        public IWishListItemService WishListItemService { get; }
        public IPaymentService PaymentService { get; }
        public ISubCategoryCategoryService SubCategoryCategoryService { get; }
        public ISubCategoryService SubCategoryService { get; }
        public ICategoryService CategoryService { get; }
        public IAddressService AddressService { get; }
        public IUserProfileService UserProfileService { get; }
        public IGroupMemberService GroupMemberService { get; }
        public IAuthService AuthService { get; }
        public ISellerRestrictionService SellerRestrictionService { get; }
        public IBuyerProfileService BuyerProfileService { get; }
        public ISellerProfileService SellerProfileService { get; }
        public ISellerViolationService SellerViolationService { get; }
        public IUserGroupSellerService UserGroupSellerService { get; }
        public IUserGroupNotificationService UserGroupNotificationService { get; }
        public ICategoryAttributeService CategoryAttributeService { get; }
        public IProductAttributeValueService ProductAttributeValueService { get; }
        public IUserOtpService UserOtpService { get; }
        public IGeneralCategoryService GeneralCategoryService { get; }
        public ITradeService TradeService { get; }
        public ITradeAttributeValueService TradeAttributeValueService { get; }
        public ITradeImageService TradeImageService { get; }
        public IGroupSellerService GroupSellerService { get; }
        public IHomePageCardService HomePageCardService { get; }
        public IUserPreferenceService UserPreferenceService { get; }
        public IProductReviewService ProductReviewService { get; }
        public ITradeReviewService TradeReviewService { get; }
        public IProductImpressionService ProductImpressionService { get; }
        public ITradeImpressionService TradeImpressionService { get; }
        public IGroupFeaturedProductService GroupFeaturedProductService { get; }



    }
}
