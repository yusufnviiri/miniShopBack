using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lucene
{
    public sealed class TradeSearchRequest
    {
        public string? Query { get; set; }

        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? SubCategoryCategoryId { get; set; }
        public int? CommodityClassId { get; set; }

        public Guid? SellerProfileId { get; set; }

        public bool? IsFeatured { get; set; }
        public bool? VerifiedSellerOnly { get; set; }
        public bool? WithImageOnly { get; set; }

        public TradeSortMode Sort { get; set; } = TradeSortMode.Relevance;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public enum TradeSortMode
    {
        Relevance = 0,
        NewestFirst = 1,
        HighestRated = 2,
    }
}
