using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ProductImpression
    {
        public Guid ProductImpressionId { get; set; }
        public UserProfile?UserProfile { get; set; }
        public Guid? UserProfileId { get; set; }
        public Product? Product { get; set; }
        public Guid ProductId {  get; set; }
    }
}
