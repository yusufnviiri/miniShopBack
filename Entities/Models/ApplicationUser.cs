using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [MaxLength(100)]
        public string? LastName { get; set; }
        [Required]
        public override string PhoneNumber { get; set; } = null!;
        public bool PhoneNumberVerified { get; set; }
        public bool AccountConfirmed { get; set; }
        public bool MfaEnabled { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string RecoveryPhoneNumber { get; set; }= string.Empty;
        public string RecoveryQuestion { get; set; }= string.Empty;
        public string RecoveryAnswer { get; set; }= string.Empty;
        public ICollection<GroupMember> GroupMemberships { get; set; } = new List<GroupMember>();
        public ICollection<UserRefreshToken> RefreshTokens { get; set; } = new List<UserRefreshToken>();
        public ICollection<UserDevice> Devices { get; set; } = new List<UserDevice>();
    }


}
