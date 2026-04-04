using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Models.Order;

namespace Shared.Dtos
{
    public class NewOrderDto
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; } = Guid.Empty;
        public string? DestinationAddress { get; set; }
        public Guid? UserGroupId { get; set; }
        public string? ContactPerson { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus OrderStatusDetails { get; set; } = OrderStatus.Pending;
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentProvider { get; set; } = default!;  // PayPal, Mobile Money, Stripe
        public PaymentStatus Payment { get; set; } = PaymentStatus.Pending;
        public ICollection<OrderItem>? OrderItems { get; set; } = [];

    }
}
