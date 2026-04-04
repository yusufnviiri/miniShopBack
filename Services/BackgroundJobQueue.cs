using Contracts.Service;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Services
{
    public class BackgroundJobQueue : IBackgroundJobQueue
    {
        private readonly Channel<ImageJob> _queue = Channel.CreateUnbounded<ImageJob>();

        public void Enqueue(ImageJob job)
            => _queue.Writer.TryWrite(job);

        public async ValueTask<ImageJob> DequeueAsync(CancellationToken token)
            => await _queue.Reader.ReadAsync(token);
    }

}
