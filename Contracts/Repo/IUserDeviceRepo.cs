using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IUserDeviceRepo
    {
        Task<UserDevice?> FindUserDevice(string  DeviceId, string UserId, bool tracking);
        Task<IEnumerable<UserDevice>> GetAllUserDevices();
        UserDevice CreateUserDevice(UserDevice device );
        void UpdateUserDevice(UserDevice device);
        void DeleteUserDevice(UserDevice device);
    }
}
