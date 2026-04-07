using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class SharedUpdatesDto
    {
        public Guid ItemId { get; set; }
        public string ItemDescription { get; set; }=string.Empty;
        public decimal Price { get; set; }=0M;

    }
}
