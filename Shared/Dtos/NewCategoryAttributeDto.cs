using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class NewCategoryAttributeDto
    {
        public int CategoryAttributeId { get; set; }
        public string AttributeName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int AttributeDataTypeId { get; set; }
    }
}
