using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    internal static class ProductIndexFields
    {
        public const string ProductId = "ProductId";
        public const string Slug = "Slug";

        public const string ProductName = "ProductName";
        public const string ProductNameSort = "ProductName_sort"; // lowercased copy for sorting
        public const string Description = "Description";
        public const string Condition = "Condition";

        public const string IsFeatured = "IsFeatured";
        public const string HasImage = "HasImage";

        public const string CategoryId = "CategoryId";
        public const string SubCategoryId = "SubCategoryId";
        public const string SubCategoryCategoryId = "SubCategoryCategoryId";
        public const string CategoryName = "CategoryName";
        public const string SubCategoryName = "SubCategoryName";
        public const string SubCategoryCategoryName = "SubCategoryCategoryName";
        public const string CommodityClassId = "CommodityClassId";

        public const string PriceMinor = "PriceMinor";
        public const string OldPriceMinor = "OldPriceMinor";

        public const string CreatedAtTicks = "CreatedAtTicks";

        public const string ProductImageId = "ProductImageId";
        public const string SellerProfileId = "SellerProfileId";
        public const string SellerId = "SellerId";
        public const string SellerName = "SellerName";
        public const string SellerSlug = "SellerSlug";
        public const string WhatsAppNumber = "WhatsAppNumber";
        public const string SellerTypeId = "SellerTypeId";
        public const string SellerTierId = "SellerTierId";
        public const string IsVerified = "IsVerified";

        public const string ReviewCount = "ReviewCount";
        public const string AverageRating = "AverageRating";
        public const string SellerLocation = "SellerLocation";
        public const string ProductNamePrefix = "ProductName_prefix";


        // Catch-all field for free-text search ("red shoes" should match across
        // name, description, brand, category without us OR-ing five clauses).
        public const string CatchAll = "_all";
    }
}
