using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class WishListItem
    {
        public Guid WishListItemId { get; set; }
        public Guid WishListId { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; } = null;
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public int Priority { get; set; } = 0; // optional
        public string? Note { get; set; }
    }

}
