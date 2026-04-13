using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class UserPreferenceDto
    {
        public int CategoryId { get; set; }
        public Guid UserProfileId { get; set; }
        public string PreferredCategory { get; set; } = string.Empty;
    }
}
