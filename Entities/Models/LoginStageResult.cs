using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class LoginStageResult
    {
        public string UserId { get; set; } = null!;
        public bool RequiresOtp { get; set; }
        public string? DeviceChallengeId { get; set; }
    }

}
