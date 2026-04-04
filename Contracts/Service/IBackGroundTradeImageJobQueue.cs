using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IBackGroundTradeImageJobQueue
    {
        void Enqueue(TradeImageJob job);
        ValueTask<TradeImageJob> DequeueAsync(CancellationToken token);
    }
}
