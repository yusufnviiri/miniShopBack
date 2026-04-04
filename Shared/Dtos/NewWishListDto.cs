using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class NewWishListDto
    {
        public Guid UserId { get; set; }
        public ICollection<Guid> Products { get; set; } = [];
    }
}
