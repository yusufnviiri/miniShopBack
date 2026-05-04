using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lucene
{
    public record ProductSearchResponse(     IReadOnlyList<ProductSearchResult> Items,
     int TotalHits,
     int Page,
     int PageSize,
     int TotalPages
 );
}
