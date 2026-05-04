using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lucene
{
    public record ProductSearchRequest(
    string? Query = null,
    string? Category = null,
    string? SubCategory = null,
    string? Condition = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    bool? IsFeatured = null,
    bool? HasImage = null,
    string SortBy = "relevance",   // relevance | price_asc | price_desc | newest
    int Page = 1,
    int PageSize = 20
);
}
