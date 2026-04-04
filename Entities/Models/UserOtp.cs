using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Index(nameof(UserId))]
    public class UserOtp
    {
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public string CodeHash { get; set; } = null!;

        public string Purpose { get; set; } = null!;
        // Login, Register, ResetPassword, VerifyPhone
        public DateTime ExpiresAt { get; set; }

        public int AttemptCount { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
        public string? DeviceId { get; set; }
        public bool Used { get; set; }
    }

}
