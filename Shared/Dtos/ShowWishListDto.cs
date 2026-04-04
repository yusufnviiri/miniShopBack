using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShowWishListDto
    {
        public ICollection<ShowProductMiniDetailsDto> SelectedItems { get; set; } = [];
        public Guid WishListId { get; set; }
        public Guid UserId { get; set; }
    }
}
