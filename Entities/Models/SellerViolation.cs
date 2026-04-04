using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SellerViolation
    {
        public Guid SellerViolationId { get; set; }
        public required Guid SellerProfileId { get; set; }
    }
}
