using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserStatus
    {
        public int UserStatusId { get; set; }
        public string StatusName { get; set; } = null!;
    }
}
