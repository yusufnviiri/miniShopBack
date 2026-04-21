using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class NewGroupFeaturedProductDto
    {
        

        public Guid UserGroupId { get; set; } = Guid.Empty;
        public Guid ProductId { get; set; } = Guid.Empty;


    }
}
