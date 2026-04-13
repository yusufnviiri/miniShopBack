using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserPreferenceCategory
    {
        public Guid UserPreferenceId { get; set; }
        public UserPreference? UserPreference { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
