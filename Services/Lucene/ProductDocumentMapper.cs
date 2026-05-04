using Contracts.Lucene;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class ProductDocumentMapper
    {
        public ProductIndexDocument Map(
            Product product,
            int reviewCount,
            float averageRating)
        {
            // The product MUST be loaded with these includes:
            //   .Include(p => p.SellerProfile)
            //   .Include(p => p.Category)
            //   .Include(p => p.SubCategory)
            //   .Include(p => p.SubCategoryCategory)
            //   .Include(p => p.Images)
            // Otherwise nav properties will be null and we'll silently index empty fields.

            if (product.SellerProfile is null)
                throw new InvalidOperationException(
                    $"Product {product.ProductId} has no SellerProfile loaded — " +
                    "ensure .Include(p => p.SellerProfile) is in the query.");

            var primaryImage = product.Images?.FirstOrDefault(); // or your "primary" rule
            var productImageId = primaryImage?.ProductImageId ?? new Guid();
            // ↑ adjust to your ProductImage shape — I don't know its exact properties

            return new ProductIndexDocument
            {
                ProductId = product.ProductId,
                Slug = product.Slug,

                ProductName = product.ProductName,
                Description = product.Description,
                Condition = product.Condition,

                IsFeatured = product.IsFeatured,
                HasImage = product.HasImage,

                CategoryId = product.CategoryId,
                SubCategoryId = product.SubCategoryId,
                SubCategoryCategoryId = product.SubCategoryCategoryId,
                CategoryName = product.Category?.CategoryName ?? string.Empty,
                SubCategoryName = product.SubCategory?.SubCategoryName ?? string.Empty,
                SubCategoryCategoryName = product.SubCategoryCategory?.SubCategoryCategoryName ?? string.Empty,
                CommodityClassId = product.CommodityClassId,

                // UGX has no minor unit — store as-is. If you ever add USD/EUR,
                // multiply by 100 and store cents.
                PriceMinor = (long)product.Price,
                OldPriceMinor = (long)product.OldPrice,

                CreatedAtTicks = product.CreatedAt.Ticks,

                ProductImageId = productImageId,

                SellerProfileId = product.SellerProfile.SellerProfileId,
                SellerId = product.SellerProfile.SellerId,
                SellerName = product.SellerProfile.SellerName,
                SellerSlug = product.SellerProfile.Slug,
                WhatsAppNumber = product.SellerProfile.WhatsAppNumber,
                SellerTypeId = product.SellerProfile.SellerTypeId,
                SellerTierId = product.SellerProfile.SellerTierId,
                IsVerified = product.SellerProfile.IsVerified,

                ReviewCount = reviewCount,
                AverageRating = averageRating,
            };
        }
    }
}
