using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserPreferenceSellerProfile
    {
        public Guid UserPreferenceId { get; set; }
        public UserPreference? UserPreference { get; set; }

        public Guid SellerProfileId { get; set; }
        public SellerProfile? SellerProfile { get; set; }
    }
}
