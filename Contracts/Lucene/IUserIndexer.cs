using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface IUserIndexer
    {
        ValueTask QueueIndexAsync(Guid userProfileId, CancellationToken ct = default);
        ValueTask QueueRemoveAsync(Guid userProfileId, CancellationToken ct = default);

        /// <summary>
        /// When a UserGroup is renamed, every member's denormalized
        /// UserGroupNames must be refreshed.
        /// </summary>
        ValueTask QueueReindexByUserGroupAsync(Guid userGroupId, CancellationToken ct = default);
    }
}
