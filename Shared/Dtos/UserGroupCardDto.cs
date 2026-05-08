using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public sealed class UserGroupCardDto
    {
        public Guid UserGroupId { get; set; }
        public string UserGroupName { get; set; } = string.Empty;
        public string SlugName { get; set; } = string.Empty;
        public string AboutGroup { get; set; } = string.Empty;
        public string GroupType { get; set; } = string.Empty;
        public int GroupTypeId { get; set; }
        public string Company { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public int MemberCount { get; set; }
        public float Score { get; set; }
    }
}
