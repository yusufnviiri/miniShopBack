using Shared.Dtos;
using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface IProductSearchService
    {
        Task<ProductSearchResult> SearchAsync(
            ProductSearchRequest request,
            CancellationToken ct = default);
    }
}
