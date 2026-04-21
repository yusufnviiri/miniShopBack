using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class GroupFeaturedProduct
    {
        public int GroupFeaturedProductId { get; set; }
        public Guid ProductId { get; set; } = Guid.Empty;
        public Product? Product { get; set; }
        public Guid UserGroupId { get; set; } = Guid.Empty;
        public UserGroup? UserGroup { get; set; }

    }
}
