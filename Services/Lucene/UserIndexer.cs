using Contracts.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class UserIndexer : IUserIndexer
    {
        private readonly UserIndexingQueue _queue;

        public UserIndexer(UserIndexingQueue queue) => _queue = queue;

        public ValueTask QueueIndexAsync(Guid userProfileId, CancellationToken ct = default)
        {
            if (userProfileId == Guid.Empty)
                throw new ArgumentException("UserProfileId cannot be empty.", nameof(userProfileId));
            return _queue.EnqueueAsync(new UserIndexingJob(UserIndexingJobType.IndexUser, userProfileId), ct);
        }

        public ValueTask QueueRemoveAsync(Guid userProfileId, CancellationToken ct = default)
        {
            if (userProfileId == Guid.Empty)
                throw new ArgumentException("UserProfileId cannot be empty.", nameof(userProfileId));
            return _queue.EnqueueAsync(new UserIndexingJob(UserIndexingJobType.RemoveUser, userProfileId), ct);
        }

        public ValueTask QueueReindexByUserGroupAsync(Guid userGroupId, CancellationToken ct = default)
        {
            if (userGroupId == Guid.Empty)
                throw new ArgumentException("UserGroupId cannot be empty.", nameof(userGroupId));
            return _queue.EnqueueAsync(new UserIndexingJob(UserIndexingJobType.ReindexByUserGroup, userGroupId), ct);
        }
    }
}
