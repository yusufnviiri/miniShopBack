using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public sealed class SlugInfo
    {
        public string Slug { get; set; } = "";
        public DateTime UpdatedAt { get; set; }
    }
}
