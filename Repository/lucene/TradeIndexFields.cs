using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    internal static class TradeIndexFields
    {
        public const string TradeId = "TradeId";
        public const string Slug = "Slug";

        public const string TradeName = "TradeName";
        public const string TradeNameSort = "TradeName_sort";
        public const string Description = "Description";

        public const string IsFeatured = "IsFeatured";
        public const string HasImage = "HasImage";
        public const string IsModified = "IsModified";
        public const string IsVerified = "IsVerified";

        public const string CategoryId = "CategoryId";
        public const string SubCategoryId = "SubCategoryId";
        public const string SubCategoryCategoryId = "SubCategoryCategoryId";
        public const string CategoryName = "CategoryName";
        public const string SubCategoryName = "SubCategoryName";
        public const string SubCategoryCategoryName = "SubCategoryCategoryName";
        public const string CommodityClassId = "CommodityClassId";

        public const string CreatedAtTicks = "CreatedAtTicks";

        public const string TradeImageId = "TradeImageId";

        public const string SellerProfileId = "SellerProfileId";
        public const string SellerId = "SellerId";
        public const string SellerName = "SellerName";
        public const string SellerSlugName = "SellerSlugName";
        public const string WhatsAppNumber = "WhatsAppNumber";
        public const string SellerTypeId = "SellerTypeId";
        public const string SellerTierId = "SellerTierId";

        public const string ReviewCount = "ReviewCount";
        public const string AverageRating = "AverageRating";
        public const string TradeNamePrefix = "TradeName_prefix";
    }
}
