using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class UserGroupNotificationDto
    {
        public int UserGroupNotificationId { get; set; }
        public Guid UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
