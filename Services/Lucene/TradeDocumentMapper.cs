using Contracts.Lucene;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class TradeDocumentMapper
    {
        public TradeIndexDocument Map(
            Trade trade,
            int reviewCount,
            float averageRating)
        {
            // The trade MUST be loaded with these includes:
            //   .Include(t => t.SellerProfile)
            //   .Include(t => t.Category)
            //   .Include(t => t.SubCategory)
            //   .Include(t => t.SubCategoryCategory)
            //   .Include(t => t.Images)
            if (trade.SellerProfile is null)
                throw new InvalidOperationException(
                    $"Trade {trade.TradeId} has no SellerProfile loaded — " +
                    "ensure .Include(t => t.SellerProfile) is in the query.");

            // Pick the primary image — same rule as products: prefer IsPrimary,
            // prefer processed, stable tiebreak by CreatedAt, fallback to any image.
            var primaryImageId = trade.Images?
                .Where(i => i.IsPrimary)
                .OrderByDescending(i => i.IsProcessed)
                .ThenBy(i => i.CreatedAt)
                .Select(i => i.TradeImageId)         // adjust to your TradeImage PK
                .FirstOrDefault();

            if (primaryImageId == Guid.Empty)
            {
                primaryImageId = trade.Images?
                    .OrderByDescending(i => i.IsProcessed)
                    .ThenBy(i => i.CreatedAt)
                    .Select(i => i.TradeImageId)
                    .FirstOrDefault() ?? Guid.Empty;
            }

            return new TradeIndexDocument
            {
                TradeId = trade.TradeId,
                Slug = trade.Slug,

                TradeName = trade.TradeName,
                Description = trade.Description,

                CategoryId = trade.CategoryId,
                SubCategoryId = trade.SubCategoryId,
                SubCategoryCategoryId = trade.SubCategoryCategoryId,
                CategoryName = trade.Category?.CategoryName ?? string.Empty,
                SubCategoryName = trade.SubCategory?.SubCategoryName ?? string.Empty,
                SubCategoryCategoryName = trade.SubCategoryCategory?.SubCategoryCategoryName ?? string.Empty,
                CommodityClassId = trade.CommodityClassId,

                CreatedAtTicks = trade.CreatedAt.Ticks,

                TradeImageId = primaryImageId ?? Guid.Empty,

                IsFeatured = trade.IsFeatured,
                HasImage = primaryImageId != Guid.Empty,  // derived, like products
                IsModified = trade.IsModified,

                SellerProfileId = trade.SellerProfile.SellerProfileId,
                SellerId = trade.SellerProfile.SellerId,
                SellerName = trade.SellerProfile.SellerName,
                SellerSlugName = trade.SellerProfile.Slug,
                WhatsAppNumber = trade.SellerProfile.WhatsAppNumber,
                SellerTypeId = trade.SellerProfile.SellerTypeId,
                SellerTierId = trade.SellerProfile.SellerTierId,
                IsVerified = trade.SellerProfile.IsVerified,

                ReviewCount = reviewCount,
                AverageRating = averageRating,
            };
        }
    }
}
