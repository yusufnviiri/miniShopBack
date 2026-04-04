using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class UserGroupNotificationRepo:RepositoryBase<UserGroupNotification>, IUserGroupNotificationRepo
    {
        public UserGroupNotificationRepo(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
      public async  Task<IEnumerable<UserGroupNotificationDto>> GetAllGroupNotifications()
        {
            return await FindAll(false)
                .Select(ugn => new UserGroupNotificationDto
                {
                    UserGroupNotificationId = ugn.UserGroupNotificationId,
                    UserGroupId = ugn.UserGroupId,
                    UserGroup = ugn.UserGroup,
                    UserProfileId = ugn.UserProfileId,
                    UserProfile = ugn.UserProfile,
                    Message = ugn.Message
                }).ToListAsync();
        }
        public async Task<UserGroupNotification?> FindGroupNotificationById(int notificationId, bool tracking)
        {
            return await FindByCondition(ugn => ugn.UserGroupNotificationId == notificationId, tracking)
                .FirstOrDefaultAsync();
        }
        public void CreateGroupNotification(UserGroupNotification notification)=>CreateBase(notification);
        public void UpdateGroupNotification(UserGroupNotification notification)=>UpdateBase(notification);
        public void DeleteGroupNotification(UserGroupNotification notification)=>DeleteBase(notification);
    }
}
