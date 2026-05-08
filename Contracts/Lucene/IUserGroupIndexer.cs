using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface IUserGroupIndexer
    {
        ValueTask QueueIndexAsync(Guid userGroupId, CancellationToken ct = default);
        ValueTask QueueRemoveAsync(Guid userGroupId, CancellationToken ct = default);
    }
}
