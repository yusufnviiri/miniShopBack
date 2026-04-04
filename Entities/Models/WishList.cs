using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class WishList
    {
        public Guid WishListId { get; set; }
        public Guid UserId { get; set; }
        public ICollection<WishListItem> WishListItems { get; set; } = new List<WishListItem>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
