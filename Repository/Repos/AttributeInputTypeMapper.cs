using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public static class AttributeInputTypeMapper
    {
        private static readonly IReadOnlyDictionary<string, AttributeInputType> Map =
            new Dictionary<string, AttributeInputType>(StringComparer.OrdinalIgnoreCase)
            {
                ["string"] = AttributeInputType.String,
                ["int"] = AttributeInputType.Int,
                ["decimal"] = AttributeInputType.Decimal,
                ["bool"] = AttributeInputType.Bool,
                ["date"] = AttributeInputType.Date
            };

        public static AttributeInputType Resolve(string? dataTypeName)
        {
            if (string.IsNullOrWhiteSpace(dataTypeName))
                return AttributeInputType.String;

            return Map.TryGetValue(dataTypeName, out var inputType)
                ? inputType
                : AttributeInputType.String;
        }

        public static string ResolveStringValue(string? dataTypeName)
        {
            return Resolve(dataTypeName).ToHtmlInputType();
        }
    }

}
