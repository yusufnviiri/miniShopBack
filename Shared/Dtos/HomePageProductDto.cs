using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class HomePageProductDto
    {
        public Guid ProductId {  get; set; }
        public string ProductName { get; set; }= string.Empty;
        public Guid SellerProfileId {  get; set; }
        public Decimal Price { get; set; }
        public Guid ProductImageId {  get; set; }
        public decimal OldPrice { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }



    }
}
