using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public  class UserOtpRepo : RepositoryBase<UserOtp>, IUserOtpRepo
    {
        public UserOtpRepo(ApplicationDbContext _db) : base(_db)
        {

        }

      public async  Task<UserOtp?> FindUserOtp(string id, bool tracking)=>await FindByCondition(k=>k.UserId==id,tracking).FirstOrDefaultAsync();
        public async Task<IEnumerable<UserOtp>> GetAllUserOtps()=>await FindAll(false).ToListAsync();

        public UserOtp CreateUserOtp(UserOtp userOtp)
        {
            CreateBase(userOtp);
            return userOtp;
        }
        public void UpdateUserOtp(UserOtp userOtp)=>UpdateBase(userOtp);
        public void DeleteUserOtp(UserOtp userOtp)=> DeleteBase(userOtp);
        public async Task<UserOtp?> FindUserOtpWithPurpose(string id, string purpose, bool tracking)
        {
            return await FindByCondition(p=>p.UserId==id&&p.Purpose==purpose&&!p.Used&&p.ExpiresAt>DateTime.UtcNow,tracking).OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        }
        public async Task<UserOtp?> FindUserOtpWithPurposeAndDeviceId(string id, string purpose, string deviceId, bool tracking)
        {
            return await FindByCondition(p => p.UserId == id && p.Purpose == purpose && p.DeviceId == deviceId && !p.Used && p.ExpiresAt > DateTime.UtcNow, tracking).OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        }




    }
}
