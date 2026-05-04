using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lucene
{
    public record ProductSearchResult(
    Guid ProductId,
    string ProductName,
    string CategoryName,
    string SubCategoryName,
    string Condition,
    decimal Price,
    decimal OldPrice,
    string Slug,
    bool IsFeatured,
    bool HasImage,
    float Score
);
}
