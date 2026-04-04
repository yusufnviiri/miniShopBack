using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Order
    {

        public Guid OrderId { get; set; }
 
        public string? DestinationAddress { get; set; }
        public string? ContactPerson { get; set; }//owner or representative of the user group
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus OrderStatusDetails { get; set; } = OrderStatus.Pending;
        public decimal DeliveryFee { get; set; }
        public ApplicationUser? User { get; set; }
        public string? ApplicationUserId {get;set;}
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public Product? Product { get; set; } 
        public string PayementMode { get; set; } = default!;  // PayPal, Mobile Money, Stripe
        public PaymentStatus Payment { get; set; } = PaymentStatus.Pending;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();   
      
     



        public Guid PlacedByProfileId { get; set; }
        public UserProfile PlacedByProfile { get; set; } = null!;

        // Optional: group context (SACCO purchase)
        public Guid? UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }



        // Money (snapshots)
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
