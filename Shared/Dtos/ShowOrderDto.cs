using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Models.Order;

namespace Shared.Dtos
{
    public sealed class ShowOrderDto
    {
        public Guid OrderId { get; init; }
        public string ? ApplicationUserId { get; init; }
        public string? UserName { get; init; }
        public string? DestinationAddress { get; init; }
        public string? UserGroupDetails { get; init; }
        public string? ContactPerson { get; init; }
        public DateTime OrderDate { get; init; }
        public OrderStatus OrderStatusDetails { get; init; }
        public decimal Subtotal { get; init; }
        public decimal DeliveryFee { get; init; }
        public decimal DiscountAmount { get; init; }
        public decimal TotalAmount { get; init; }
        public string PayementMode { get; init; } = default!;
        public PaymentStatus Payment { get; init; }
        public IReadOnlyCollection<ShowOrderItemDto> OrderItems { get; init; }
            = [];
    }
}
