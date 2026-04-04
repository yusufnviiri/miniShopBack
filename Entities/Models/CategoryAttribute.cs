using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CategoryAttribute
    {
        public int CategoryAttributeId { get; set; }
        public string AttributeName { get; set; } = string.Empty;    
        public int CategoryId { get; set; }
        public int AttributeDataTypeId { get; set; }
        public AttributeDataType? AttributeDataType { get; set; }
        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = [];
        public ICollection<TradeAttributeValue> TradeAttributeValues  { get; set; } = [];

    }
}
