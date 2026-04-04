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
    public class RefreshTokenRepo : RepositoryBase<UserRefreshToken>, IRefreshTokenRepo
    {
        public RefreshTokenRepo(ApplicationDbContext context) : base(context)
        {

        }

       public async Task<IEnumerable<UserRefreshToken>> GetAllUserRefreshTokens()=>await FindAll(false).ToListAsync();
        public UserRefreshToken CreateUserRefreshToken(UserRefreshToken token) { CreateBase(token); return token;  }
        public void UpdateUserRefreshToken(UserRefreshToken token)=>UpdateBase(token);
        public void DeleteUserRefreshToken(UserRefreshToken token)=>DeleteBase(token);
        public async Task<UserRefreshToken?> FindUserRefreshToken(string id, bool tracking)=>await FindByCondition(t=>t.UserId==id,tracking).FirstOrDefaultAsync();
        public async Task<IEnumerable<UserRefreshToken?>> FindUserRefreshTokensByDeviceId(string deviceId, bool tracking)=>await FindByCondition(x =>x.DeviceId == deviceId &&x.RevokedAt == null && x.ExpiresAt > DateTime.UtcNow, tracking).ToListAsync();

        public async Task<IEnumerable<UserRefreshToken?>> FindUserRefreshTokensByUserIdAndDeviceId(string userId, string deviceId, bool tracking) => await FindByCondition(x => x.UserId == userId && x.DeviceId == deviceId, tracking).ToListAsync();
        public async Task<IEnumerable<UserRefreshToken?>> FindUserRefreshTokensByUserId(string userId, bool tracking) => await FindByCondition(x => x.UserId == userId,tracking).ToListAsync();
    }
}
