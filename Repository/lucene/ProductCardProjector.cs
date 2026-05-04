using Lucene.Net.Documents;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    internal static class ProductCardProjector
    {
        public static ProductCardDto Project(Document doc, float score)
        {
            return new ProductCardDto
            {
                ProductId = ParseGuidN(doc.Get(ProductIndexFields.ProductId)),
                Slug = doc.Get(ProductIndexFields.Slug) ?? string.Empty,

                ProductName = doc.Get(ProductIndexFields.ProductName) ?? string.Empty,
                Description = doc.Get(ProductIndexFields.Description) ?? string.Empty,
                Condition = doc.Get(ProductIndexFields.Condition) ?? string.Empty,
                IsFeatured = doc.Get(ProductIndexFields.IsFeatured) == "1",

                Price = doc.GetField(ProductIndexFields.PriceMinor)?.GetInt64Value() ?? 0,
                OldPrice = doc.GetField(ProductIndexFields.OldPriceMinor)?.GetInt64Value() ?? 0,
                PrimaryImageUrl = doc.Get(ProductIndexFields.ProductImageId) ?? string.Empty,

                CategoryName = doc.Get(ProductIndexFields.CategoryName) ?? string.Empty,
                SubCategoryName = doc.Get(ProductIndexFields.SubCategoryName) ?? string.Empty,
                SubCategoryCategoryName = doc.Get(ProductIndexFields.SubCategoryCategoryName) ?? string.Empty,

                SellerProfileId = ParseGuidN(doc.Get(ProductIndexFields.SellerProfileId)),
                SellerId = ParseGuidN(doc.Get(ProductIndexFields.SellerId)),
                SellerName = doc.Get(ProductIndexFields.SellerName) ?? string.Empty,
                SellerSlug = doc.Get(ProductIndexFields.SellerSlug) ?? string.Empty,
                WhatsAppNumber = doc.Get(ProductIndexFields.WhatsAppNumber) ?? string.Empty,
                SellerTypeId = doc.GetField(ProductIndexFields.SellerTypeId)?.GetInt32Value() ?? 0,
                SellerTierId = doc.GetField(ProductIndexFields.SellerTierId)?.GetInt32Value() ?? 0,
                IsVerified = doc.Get(ProductIndexFields.IsVerified) == "1",

                ReviewCount = doc.GetField(ProductIndexFields.ReviewCount)?.GetInt32Value() ?? 0,
                AverageRating = doc.GetField(ProductIndexFields.AverageRating)?.GetSingleValue() ?? 0f,

                Score = score,
            };
        }

        private static Guid ParseGuidN(string? s) =>
            string.IsNullOrEmpty(s) ? Guid.Empty : Guid.ParseExact(s, "N");
    }
}
