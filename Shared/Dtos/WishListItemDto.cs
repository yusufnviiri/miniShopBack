using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class WishListItemDto
    {
        public Guid WishListItemId { get; set; }
        public Guid WishListId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public string? Note { get; set; }
    }
}
