using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class TradeIndexingQueue
    {
        private readonly Channel<TradeIndexingJob> _channel =
            Channel.CreateUnbounded<TradeIndexingJob>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false,
            });

        public ValueTask EnqueueAsync(TradeIndexingJob job, CancellationToken ct)
            => _channel.Writer.WriteAsync(job, ct);

        public IAsyncEnumerable<TradeIndexingJob> ReadAllAsync(CancellationToken ct)
            => _channel.Reader.ReadAllAsync(ct);
    }

    public enum TradeIndexingJobType
    {
        IndexTrade,
        RemoveTrade,
        ReindexBySeller,
    }

    public sealed record TradeIndexingJob(TradeIndexingJobType Type, Guid Id);
}
