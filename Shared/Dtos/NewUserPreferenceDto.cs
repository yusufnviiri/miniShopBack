using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class NewUserPreferenceDto
    {
        public Guid UserProfileId { get; set; }
        public Guid? SellerProfile { get; set; }
        public ICollection<int>? CategoryIds { get; set; } = [];
    }
}
