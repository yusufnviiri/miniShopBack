using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
       public enum PaymentStatus
    {
        Initiated,          // created in your system
        Pending,            // provider accepted, waiting confirmation
        Completed,          // money received
        Failed,             // definitively failed
        Refunded
    }
}
