using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public User? Payer { get; set; } = null;
        public UserGroup? PayerGroup { get; set; } = null;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string PaymentMethod { get; set; } = default!; // e.g., Credit Card, PayPal, Mobile Money
         public Guid OrderId { get; set; }
        public Order? Order { get; set; } = null;
    }
}
