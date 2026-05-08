using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class UserGroupIndexingQueue
    {
        private readonly Channel<UserGroupIndexingJob> _channel =
            Channel.CreateUnbounded<UserGroupIndexingJob>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false,
            });

        public ValueTask EnqueueAsync(UserGroupIndexingJob job, CancellationToken ct)
            => _channel.Writer.WriteAsync(job, ct);

        public IAsyncEnumerable<UserGroupIndexingJob> ReadAllAsync(CancellationToken ct)
            => _channel.Reader.ReadAllAsync(ct);
    }

    public enum UserGroupIndexingJobType { IndexGroup, RemoveGroup }
    public sealed record UserGroupIndexingJob(UserGroupIndexingJobType Type, Guid Id);
}
