using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class UserDeviceService:IUserDeviceService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;


        public UserDeviceService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }


       public async Task<UserDevice?> FindUserDevice(string DeviceId, string UserId, bool tracking)=> await _repoManager.UserDeviceRepo.FindUserDevice(DeviceId,UserId,tracking);
        public async Task<IEnumerable<UserDevice>> GetAllUserDevices()=>await _repoManager.UserDeviceRepo.GetAllUserDevices();
        public async Task<UserDevice> CreateUserDevice(UserDevice device)
        {
          var newDevice=  _repoManager.UserDeviceRepo.CreateUserDevice(device);
            await _repoManager.SaveRepoDataAsync();
            return newDevice;
        }
        public async Task UpdateUserDevice(UserDevice device)
        {

            var userDevice = await _repoManager.UserDeviceRepo.FindUserDevice(device.DeviceId, device.UserId,false);
            if (userDevice == null) { return; }
            var updated = _mapper.Map<UserDevice>(userDevice);
            _repoManager.UserDeviceRepo.UpdateUserDevice(updated);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteUserDevice(string deviceId, string userId)
        {
            var device = await _repoManager.UserDeviceRepo.FindUserDevice(deviceId,userId, true);
            if (device == null) return;
            _repoManager.UserDeviceRepo.UpdateUserDevice(device);
            await _repoManager.SaveRepoDataAsync();

        }
    }
}
