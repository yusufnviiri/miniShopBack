using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class LoggedInUserDataDto
    {
        public Guid UserProfileId { get; set; } = Guid.Empty;
        public Guid SellerprofileId { get; set; } = Guid.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool IsAccountConfirmed { get; set; }
        public string IdentityRole { get; set; } = string.Empty;
        public ICollection<GroupMemberRolesDto>? UserGroupData { get; set; }
    }
}
