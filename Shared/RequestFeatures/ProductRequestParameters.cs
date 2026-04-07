using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class ProductRequestParameters : RequestParameters
    {
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; } = decimal.Zero;
        public string ProductName { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public string ProductDescription { get; set; }=string.Empty;

    }
}
