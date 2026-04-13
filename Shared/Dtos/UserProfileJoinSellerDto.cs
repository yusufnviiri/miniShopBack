using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class UserProfileJoinSellerDto
    {
        public Guid UserProfileId { get; set; }
        public Guid SellerProfileId { get; set; }

    }
}
