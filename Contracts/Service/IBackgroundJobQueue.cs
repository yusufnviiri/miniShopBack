using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{

    public interface IBackgroundJobQueue
    {
        void Enqueue(ImageJob job);
        ValueTask<ImageJob> DequeueAsync(CancellationToken token);

    }

}
