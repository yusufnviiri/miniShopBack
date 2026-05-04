using Contracts.Lucene;
using Microsoft.Extensions.Options;
using Repository.lucene;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    

        public sealed class ProductSearchService : IProductSearchService
        {
            private readonly IProductSearchRepository _repo;
            private readonly LuceneOptions _options;

            public ProductSearchService(
                IProductSearchRepository repo,
                IOptions<LuceneOptions> options)
            {
                _repo = repo;
                _options = options.Value;
            }

            public Task<ProductSearchResult> SearchAsync(
                ProductSearchRequest request,
                CancellationToken ct = default)
            {
                // Lucene calls are synchronous (CPU-bound, very fast).
                // Wrapping them in Task.Run would just waste a thread-pool thread.
                // Returning Task.FromResult is correct here.
                var result = _repo.Search(request, _options.MaxPageSize);
                return Task.FromResult(result);
            }
        }
    }
