using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class MiniProductUpdateDto
    {
        public Guid ProductId {  get; set; } = Guid.Empty;
        public int ProductStock {  get; set; }
        public decimal ProductPrice { get; set; }

    }
}
