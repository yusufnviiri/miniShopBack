using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class NewOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid OrderItemId { get; set; }= Guid.NewGuid();

    }
}
