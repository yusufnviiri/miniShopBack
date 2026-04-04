using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Index(nameof(DeviceId), IsUnique = true)]
    public class UserDevice
    {
        public Guid Id { get; set; }

        [Required]
        public string DeviceId { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public string UserAgent { get; set; } = null!;
        public string IpHash { get; set; } = null!;
        public bool IsTrusted { get; set; }
        public DateTime FirstSeenAt { get; set; } = DateTime.UtcNow;
        public DateTime LastSeenAt { get; set; } = DateTime.UtcNow;
    }

}
