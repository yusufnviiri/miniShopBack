using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string City { get; set; }=string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;    
        public string Company { get; set; } = string.Empty;
        

    }

}
