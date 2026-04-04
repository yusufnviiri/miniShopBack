using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShowApplicationUserDto
    {

        public string? IdentityUserId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Company { get; set; }
        public Guid? SellerId {  get; set; }


        public int AddressId { get; set; }



    }
}
