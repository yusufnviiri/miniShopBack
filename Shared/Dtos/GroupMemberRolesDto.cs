using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class GroupMemberRolesDto
    {
        public Guid GroupId { get; set; }   
        public string Role { get; set; }= string.Empty;
        public string GroupName { get; set; } = string.Empty;
    }
}
