using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
      internal sealed class UserGroupNotificationService : IUserGroupNotificationService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserGroupNotificationService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

       public async   Task<IEnumerable<UserGroupNotificationDto>> GetAllGroupNotificationsAsync()=>await _repoManager.UserGroupNotificationRepo.GetAllGroupNotifications();
        public async Task<UserGroupNotification?> FindGroupNotificationByIdAsync(int notificationId, bool tracking)=>await _repoManager.UserGroupNotificationRepo.FindGroupNotificationById(notificationId,tracking);
        public async Task CreateGroupNotificationAsync(UserGroupNotification notification)
        {
            _repoManager.UserGroupNotificationRepo.CreateGroupNotification(notification);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateGroupNotificationAsync(UserGroupNotification notification)
        {
            var existingNotification = await _repoManager.UserGroupNotificationRepo.FindGroupNotificationById(notification.UserGroupNotificationId, true);
            if (existingNotification == null)
            {
                _logger.LogError($"Notification with ID {notification.UserGroupNotificationId} not found.");
                throw new ObjectBadRequestExeption("Notification Violation not found.");
            }
            existingNotification.Message = notification.Message;
            existingNotification.UserGroupId = notification.UserGroupId;
            existingNotification.UserProfileId = notification.UserProfileId;

            _repoManager.UserGroupNotificationRepo.UpdateGroupNotification(existingNotification);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteGroupNotificationAsync(int notificationId)
        {
            var existingNotification = await _repoManager.UserGroupNotificationRepo.FindGroupNotificationById(notificationId, true);
            if (existingNotification == null)
            {
                _logger.LogError($"Notification with ID {notificationId} not found.");
                throw new ObjectBadRequestExeption("Notification Violation not found.");
            }
            _repoManager.UserGroupNotificationRepo.DeleteGroupNotification(existingNotification);
            await _repoManager.SaveRepoDataAsync();
        }

    }
}
