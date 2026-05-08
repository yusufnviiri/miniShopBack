using Contracts.Lucene;
using Microsoft.Extensions.Options;
using Repository.lucene;
using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class UserGroupSearchService : IUserGroupSearchService
    {
        private readonly IUserGroupSearchRepository _repo;
        private readonly LuceneOptions _options;

        public UserGroupSearchService(
            IUserGroupSearchRepository repo,
            IOptions<LuceneOptions> options)
        {
            _repo = repo;
            _options = options.Value;
        }

        public Task<UserGroupSearchResult> SearchAsync(
            UserGroupSearchRequest request,
            CancellationToken ct = default)
        {
            var result = _repo.Search(request, _options.MaxPageSize);
            return Task.FromResult(result);
        }
    }
}
