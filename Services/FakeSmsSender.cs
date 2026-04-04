using Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FakeSmsSender : ISmsSender
    {
        public Task SendAsync(string phoneNumber, string message)
        {
            Console.WriteLine($"SMS to {phoneNumber}: {message}");
            return Task.CompletedTask;
        }
    }

}
