using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User 
    {
        public Guid? UserId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Address { get;set; } = string.Empty ;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public UserGroup? UserGroup { get; set; }
        public Guid UserGroupId { get; set; }
        public bool? IsGroupMember { get; set; }=false;
        public string Role { get; set; } = default!; // Admin, Customer, Manager


    }
}
