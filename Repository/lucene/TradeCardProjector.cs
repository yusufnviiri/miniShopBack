using Lucene.Net.Documents;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    internal static class TradeCardProjector
    {
        public static TradeCardDto Project(Document doc, float score)
        {
            return new TradeCardDto
            {
                TradeId = ParseGuidN(doc.Get(TradeIndexFields.TradeId)),
                TradeName = doc.Get(TradeIndexFields.TradeName) ?? string.Empty,
                SlugName = doc.Get(TradeIndexFields.Slug) ?? string.Empty,
                Description = doc.Get(TradeIndexFields.Description) ?? string.Empty,

                IsFeatured = doc.Get(TradeIndexFields.IsFeatured) == "1",
                TradeImageId = ParseGuidN(doc.Get(TradeIndexFields.TradeImageId)),

                CategoryName = doc.Get(TradeIndexFields.CategoryName) ?? string.Empty,
                SubCategoryName = doc.Get(TradeIndexFields.SubCategoryName) ?? string.Empty,
                SubCategoryCategoryName = doc.Get(TradeIndexFields.SubCategoryCategoryName) ?? string.Empty,

                SellerProfileId = ParseGuidN(doc.Get(TradeIndexFields.SellerProfileId)),
                SellerId = ParseGuidN(doc.Get(TradeIndexFields.SellerId)),
                SellerName = doc.Get(TradeIndexFields.SellerName) ?? string.Empty,
                SellerSlugName = doc.Get(TradeIndexFields.SellerSlugName) ?? string.Empty,
                WhatsAppNumber = doc.Get(TradeIndexFields.WhatsAppNumber) ?? string.Empty,
                SellerTypeId = doc.GetField(TradeIndexFields.SellerTypeId)?.GetInt32Value() ?? 0,
                SellerTierId = doc.GetField(TradeIndexFields.SellerTierId)?.GetInt32Value() ?? 0,
                IsVerified = doc.Get(TradeIndexFields.IsVerified) == "1",

                ReviewCount = doc.GetField(TradeIndexFields.ReviewCount)?.GetInt32Value() ?? 0,
                AverageRating = doc.GetField(TradeIndexFields.AverageRating)?.GetSingleValue() ?? 0f,

                Score = score,
            };
        }

        private static Guid ParseGuidN(string? s) =>
            string.IsNullOrEmpty(s) ? Guid.Empty : Guid.ParseExact(s, "N");
    }
}
