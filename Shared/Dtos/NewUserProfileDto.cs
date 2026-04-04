using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class NewUserProfileDto
    {

        public Guid UserProfileId { get; set; }
        public string IdentityUserId { get; set; } = null!;
        public int AddressId { get; set; }
        public ICollection<GroupMember>? GroupMemberships { get; set; }
            = new List<GroupMember>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
