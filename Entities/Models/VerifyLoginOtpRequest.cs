using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class VerifyLoginOtpRequest
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string DeviceId { get; set; } = null!;

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Code { get; set; } = null!;
    }

}
