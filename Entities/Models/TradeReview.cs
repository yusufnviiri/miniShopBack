using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TradeReview
    {
        public Guid TradeReviewId { get; set; }
        public Guid TradeId { get; set; } = Guid.Empty;
        public Trade? Trade { get; set; }
        public Guid UserProfileId { get; set; } = default!;
        public UserProfile Reviewer { get; set; } = default!;
        public int Rating { get; set; } // 1–5
        public string Comment { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
