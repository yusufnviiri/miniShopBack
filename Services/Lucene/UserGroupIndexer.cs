using Contracts.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class UserGroupIndexer : IUserGroupIndexer
    {
        private readonly UserGroupIndexingQueue _queue;

        public UserGroupIndexer(UserGroupIndexingQueue queue) => _queue = queue;

        public ValueTask QueueIndexAsync(Guid userGroupId, CancellationToken ct = default)
        {
            if (userGroupId == Guid.Empty)
                throw new ArgumentException("UserGroupId cannot be empty.", nameof(userGroupId));
            return _queue.EnqueueAsync(
                new UserGroupIndexingJob(UserGroupIndexingJobType.IndexGroup, userGroupId), ct);
        }

        public ValueTask QueueRemoveAsync(Guid userGroupId, CancellationToken ct = default)
        {
            if (userGroupId == Guid.Empty)
                throw new ArgumentException("UserGroupId cannot be empty.", nameof(userGroupId));
            return _queue.EnqueueAsync(
                new UserGroupIndexingJob(UserGroupIndexingJobType.RemoveGroup, userGroupId), ct);
        }
    }
}
