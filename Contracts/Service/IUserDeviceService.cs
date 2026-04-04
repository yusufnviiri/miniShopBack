using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IUserDeviceService
    {
        Task<UserDevice?> FindUserDevice(string DeviceId, string UserId, bool tracking);
        Task<IEnumerable<UserDevice>> GetAllUserDevices();
        Task<UserDevice> CreateUserDevice(UserDevice device);
        Task UpdateUserDevice(UserDevice device);
        Task DeleteUserDevice(string deviceId, string userId);
    }
}
