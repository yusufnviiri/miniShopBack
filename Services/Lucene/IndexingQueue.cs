using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Services.Lucene
{
    /// <summary>
    /// In-memory queue of indexing jobs.
    /// Producers (ProductIndexer.QueueXxxAsync) write here; the
    /// IndexingBackgroundService drains and processes them.
    /// </summary>
    public sealed class IndexingQueue
    {
        private readonly Channel<IndexingJob> _channel =
            Channel.CreateUnbounded<IndexingJob>(new UnboundedChannelOptions
            {
                SingleReader = true,   // one background service consumes
                SingleWriter = false,  // many request threads produce
            });

        public ValueTask EnqueueAsync(IndexingJob job, CancellationToken ct)
            => _channel.Writer.WriteAsync(job, ct);

        public IAsyncEnumerable<IndexingJob> ReadAllAsync(CancellationToken ct)
            => _channel.Reader.ReadAllAsync(ct);
    }

    public enum IndexingJobType
    {
        IndexProduct,
        RemoveProduct,
        ReindexBySeller,
    }

    public sealed record IndexingJob(IndexingJobType Type, Guid Id);
}
