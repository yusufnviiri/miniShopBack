using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TradeAttributeValue
    {
        public Guid TradeId { get; set; } = Guid.Empty;
        public int TradeAttributeValueId { get; set; }
        public CategoryAttribute? CategoryAttribute { get; set; }
        public int CategoryAttributeId { get; set; }
        public string? StringValue { get; set; } = null;
        public int? IntValue { get; set; } = 0;
        public decimal? DecimalValue { get; set; } = 0;
        public bool? BoolValue { get; set; } = false;
        public DateOnly? DateValue { get; set; }
    }
}
    

