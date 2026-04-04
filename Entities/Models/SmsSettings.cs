using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SmsSettings
    {
        public string Provider { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string SenderId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;
    }

}
