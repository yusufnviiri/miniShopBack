using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ProductSearchRequest
    {

        /// <summary>Free-text search query (the search box).</summary>
        public string? Query { get; set; }

        /// <summary>Filter: category at any of the three levels.</summary>
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? SubCategoryCategoryId { get; set; }

        public int? CommodityClassId { get; set; }

        /// <summary>Filter: "New" or "Used".</summary>
        public string? Condition { get; set; }

        /// <summary>Price range (in minor units, same as indexed).</summary>
        public long? MinPrice { get; set; }
        public long? MaxPrice { get; set; }

        /// <summary>Filter: only products from a specific seller.</summary>
        public Guid? SellerProfileId { get; set; }

        /// <summary>Filter flags.</summary>
        public bool? IsFeatured { get; set; }
        public bool? VerifiedSellerOnly { get; set; }
        public bool? WithImageOnly { get; set; }

        /// <summary>Sort mode. See ProductSortMode.</summary>
        public ProductSortMode Sort { get; set; } = ProductSortMode.Relevance;

        /// <summary>1-based page number.</summary>
        public int Page { get; set; } = 1;

        /// <summary>Page size (clamped server-side to LuceneOptions.MaxPageSize).</summary>
        public int PageSize { get; set; } = 20;
    }

    public enum ProductSortMode
    {
        Relevance = 0,
        NewestFirst = 1,
        PriceLowToHigh = 2,
        PriceHighToLow = 3,
        HighestRated = 4,
    }
}
