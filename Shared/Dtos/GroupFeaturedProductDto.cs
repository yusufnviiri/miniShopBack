using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class GroupFeaturedProductDto
    {
        public int GroupFeaturedProductId { get; set; }
        public Guid ProductId { get; set; } = Guid.Empty;
        public string? ProductName { get; set; }
        public Guid UserGroupId { get; set; } = Guid.Empty;
        public string? UserGroupName { get; set; }
        public Guid? ProductImageId { get; set; }

    }
}
