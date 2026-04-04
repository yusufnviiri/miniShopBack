using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public sealed class CategoryAttributeDto
    {
        public int CategoryAttributeId { get; init; }
        public int CategoryId { get; init; }
        public int AttributeDataTypeId { get; init; }
        public string AttributeName { get; init; } = string.Empty;
        public AttributeInputType InputType { get; init; }
        public string InputTypeValue { get; init; } = string.Empty;

    }

}
