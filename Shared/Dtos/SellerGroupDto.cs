using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class SellerGroupDto
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }=string.Empty;
        public string? SellerSlugName { get; set; } = string.Empty;

    }
}
