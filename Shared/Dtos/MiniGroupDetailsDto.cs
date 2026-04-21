using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class MiniGroupDetailsDto
    {
        public Guid SellerProfileId { get; set; } = Guid.Empty;
        public Guid SellerId { get; set; } = Guid.Empty;
        public string SellerName { get; set; } = string.Empty;
        public string SellerTypeDescription { get; set; } = string.Empty;

    }
}
