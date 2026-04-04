using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Review
    {

        public Guid ReviewId { get; set; }
        public Guid ProductId { get; set; }=Guid.Empty;
        public Product? Product { get; set; }
        public Guid TradeId { get; set; }=Guid.Empty;
        public Trade? Trade { get; set; }

        public string ApplicationUserId { get; set; } = default!;
        public ApplicationUser Reviewer {  get; set; }=default!;
        public int Rating { get; set; } // 1–5
        public string Comment { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
