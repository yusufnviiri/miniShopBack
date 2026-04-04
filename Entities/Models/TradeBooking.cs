using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TradeBooking
    {
        public Guid TradeBookingId {  get; set; }
        public string Description { get; set; } = default!;
        public UserProfile? BookedBy { get; set; }
        public Guid UserProfileId { get;set; }      

        public Trade? Trade { get; set; }
        public Guid TradeId { get; set; }=Guid.Empty;
        public DateOnly DateBooked { get; set; }      

              // Pricing Snapshot (IMPORTANT — never rely on Trade price later)
        public decimal AgreedPrice { get; set; }
        public bool IsHourly { get; set; }
        public decimal? HoursBooked { get; set; }

        // Status Flow
        public BookingStatus? BookingStatus { get; set; }
        public int BookingStatusId { get;set; }

        // Payment
        //public PaymentStatus PaymentStatus { get; set; }
        //public string? PaymentReference { get; set; }

        // Cancellation
        public string? CancellationReason { get; set; }
        public DateTime? CancelledAt { get; set; }

        // Completion
        public DateTime? CompletedAt { get; set; }

        // Review
        // Metadata
        public DateTime? UpdatedAt { get; set; }




    }
}
