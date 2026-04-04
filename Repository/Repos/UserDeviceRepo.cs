using Azure.Core;
using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class UserDeviceRepo:RepositoryBase<UserDevice>,IUserDeviceRepo
    {
        public UserDeviceRepo(ApplicationDbContext context):base(context)
        {
            
        }

     public async   Task<UserDevice?> FindUserDevice(string DeviceId, string UserId, bool tracking)=>await FindByCondition(x =>x.UserId == UserId &&
        x.DeviceId == DeviceId,tracking).FirstOrDefaultAsync();
        public async Task<IEnumerable<UserDevice>> GetAllUserDevices()=>await FindAll(false).ToListAsync();
        public UserDevice CreateUserDevice(UserDevice device) { CreateBase(device);

            return device;
        }
        public void UpdateUserDevice(UserDevice device)=>UpdateBase(device);
        public void DeleteUserDevice(UserDevice device)=>DeleteBase(device);
    }
}
