using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SecurityAuditLog
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string IpHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
