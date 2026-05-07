using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface IUserSearchService
    {
        Task<UserSearchResult> SearchAsync(
            UserSearchRequest request,
            CancellationToken ct = default);
    }
}
