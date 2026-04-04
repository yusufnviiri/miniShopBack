using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class SellerRestrictionDto
    {
        public Guid SellerRestrictionId { get; set; }
        public Guid SellerProfileId { get; set; }
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddMonths(1);
        public string Reason { get; set; } = null!;

    }
}
