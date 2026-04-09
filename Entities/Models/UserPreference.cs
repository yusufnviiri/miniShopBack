using Microsoft.AspNetCore.Routing.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserPreference
    {
        public Guid UserPreferenceId { get; set; }
        public UserProfile? UserProfile { get; set; }
        public Guid UserProfileId { get; set; }  
        public ICollection<SellerProfile>? SellerProfiles  { get; set; } = [];
        public ICollection<Category>?  Categories { get; set; } = [];

    }
}
