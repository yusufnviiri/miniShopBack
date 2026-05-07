using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class UserIndexingQueue
    {
        private readonly Channel<UserIndexingJob> _channel =
            Channel.CreateUnbounded<UserIndexingJob>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false,
            });

        public ValueTask EnqueueAsync(UserIndexingJob job, CancellationToken ct)
            => _channel.Writer.WriteAsync(job, ct);

        public IAsyncEnumerable<UserIndexingJob> ReadAllAsync(CancellationToken ct)
            => _channel.Reader.ReadAllAsync(ct);
    }

    public enum UserIndexingJobType
    {
        IndexUser,
        RemoveUser,
        ReindexByUserGroup,
    }

    public sealed record UserIndexingJob(UserIndexingJobType Type, Guid Id);
}
