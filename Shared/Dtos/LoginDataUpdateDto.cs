using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class LoginDataUpdateDto
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string RecoveryPhoneNumber { get; set; } = string.Empty;
        public string RecoveryQuestion { get; set; } = string.Empty;
        public string RecoveryAnswer { get; set; } = string.Empty;

    }
}
