using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class NewApplicationUserDto
    {

        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Password { get; set; }
 
    }
}
