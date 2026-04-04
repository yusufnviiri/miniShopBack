using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ProductAttributeValue
    {
        public Guid? ProductId { get; set; }
        public int ProductAttributeValueId { get; set; }
        public CategoryAttribute? CategoryAttribute { get; set; }
        public int CategoryAttributeId { get; set; }
        public string? StringValue { get; set; }=null;
        public int? IntValue { get; set; }=0;
        public decimal? DecimalValue { get; set; }=0;
        public bool? BoolValue { get; set; } = false;
        public DateOnly? DateValue { get; set; }
    }
}
