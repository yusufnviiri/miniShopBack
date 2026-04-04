using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SellerRestriction
    {
        public Guid SellerRestrictionId { get; set; }
        public Guid SellerProfileId {  get; set; }
        public SellerProfile SellerProfile {  get; set; } = null!;
        public DateTime ExpiresAt {  get; set; }
        public string Reason {  get; set; } = null!;

    }
}
