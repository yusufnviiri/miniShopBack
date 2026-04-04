using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class TradeAttributeValueDto
    {
        public Guid? TradeId { get; set; }
        public int TradeAttributeValueId { get; set; }
        public int CategoryAttributeId { get; set; }
        public string? StringValue { get; set; } = null;
        public int? IntValue { get; set; } = 0;
        public decimal? DecimalValue { get; set; } = 0;
        public bool? BoolValue { get; set; } = false;
        public DateOnly? DateOnlyValue { get; set; }
    }
}
