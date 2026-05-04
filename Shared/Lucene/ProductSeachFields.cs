using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lucene
{
    public class ProductSeachFields
    {


        public const string ProductId = "productId";
        public const string ProductName = "productName";
        public const string Description = "description";
        public const string CategoryName = "categoryName";
        public const string SubCategoryName = "subCategoryName";
        public const string Condition = "condition";
        public const string Price = "price";
        public const string OldPrice = "oldPrice";
        public const string IsActive = "isActive";
        public const string IsFeatured = "isFeatured";
        public const string IsDeleted = "isDeleted";
        public const string HasImage = "hasImage";
        public const string Slug = "slug";
        public const string SellerProfileId = "sellerProfileId";
        public const string CreatedAt = "createdAt";

        // Composite field for full-text search
        public const string FullText = "fullText";
    }
}
