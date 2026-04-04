using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IUserGroupNotificationService
    {
        Task<IEnumerable<UserGroupNotificationDto>> GetAllGroupNotificationsAsync();
        Task<UserGroupNotification?> FindGroupNotificationByIdAsync(int notificationId, bool tracking);
        Task CreateGroupNotificationAsync(UserGroupNotification notification);
        Task UpdateGroupNotificationAsync(UserGroupNotification notification);
        Task DeleteGroupNotificationAsync(int notificationId);
    }
}
