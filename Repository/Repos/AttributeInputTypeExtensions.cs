using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public static class AttributeInputTypeExtensions
    {
        public static string ToHtmlInputType(this AttributeInputType type) =>
            type switch
            {
                AttributeInputType.String => "text",
                AttributeInputType.Int => "number",
                AttributeInputType.Decimal => "number",
                AttributeInputType.Bool => "checkbox",
                AttributeInputType.Date => "date",
                _ => "text"
            };
    }

}
