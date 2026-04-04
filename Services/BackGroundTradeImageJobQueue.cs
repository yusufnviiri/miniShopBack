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
    public class BackGroundTradeImageJobQueue:IBackGroundTradeImageJobQueue
    {
        private readonly Channel<TradeImageJob> _queue = Channel.CreateUnbounded<TradeImageJob>();

        public void Enqueue(TradeImageJob job)
            => _queue.Writer.TryWrite(job);

        public async ValueTask<TradeImageJob> DequeueAsync(CancellationToken token)
            => await _queue.Reader.ReadAsync(token);
    }
}
