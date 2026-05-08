using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface IUserGroupSearchService
    {
        Task<UserGroupSearchResult> SearchAsync(
            UserGroupSearchRequest request,
            CancellationToken ct = default);
    }
}
