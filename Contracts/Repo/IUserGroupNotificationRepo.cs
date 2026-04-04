using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IUserGroupNotificationRepo
    {
        Task<IEnumerable<UserGroupNotificationDto>> GetAllGroupNotifications();
        Task<UserGroupNotification?> FindGroupNotificationById(int notificationId, bool tracking);
        void CreateGroupNotification(UserGroupNotification notification);
        void UpdateGroupNotification(UserGroupNotification notification);
        void DeleteGroupNotification(UserGroupNotification notification);
    }
}
