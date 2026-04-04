using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserGroupNotification
    {
        public int UserGroupNotificationId { get; set; }
        public Guid UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
