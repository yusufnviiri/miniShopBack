using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class JwtSettings
    {
        [Required]
        public string SecretKey { get; set; } = string.Empty;

        [Required]
        public string ValidIssuer { get; set; } = string.Empty;

        [Required]
        public string ValidAudience { get; set; } = string.Empty;

        [Range(1, 1440)]
        public int AccessTokenMinutes { get; set; }
    }

}
