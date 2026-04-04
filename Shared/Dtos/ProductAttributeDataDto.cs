using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ProductAttributeDataDto
    {

        public int ProductAttributeValueId { get; set; }
        public int CategoryAttributeId { get; set; }
        public string AttributeDataType { get; set; }= string.Empty;
        public string AttributeName { get; set; } = string.Empty;
        public string? StringValue { get; set; } = null;
        public int? IntValue { get; set; } = 0;
        public decimal? DecimalValue { get; set; } = 0;
        public bool? BoolValue { get; set; } = false;
        public DateOnly? DateOnlyValue { get; set; }
    }
}

