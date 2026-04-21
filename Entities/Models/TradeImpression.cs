using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TradeImpression
    {
        public Guid TradeImpressionId { get; set; }
        public UserProfile? UserProfile { get; set; }
        public Guid? UserProfileId { get; set; }
        public Trade? Trade { get; set; }
        public Guid TradeId { get; set; }
    }
}
