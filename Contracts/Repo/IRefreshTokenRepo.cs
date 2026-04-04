using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IRefreshTokenRepo
    {
        Task<IEnumerable<UserRefreshToken>> GetAllUserRefreshTokens();
        UserRefreshToken CreateUserRefreshToken(UserRefreshToken token );
        void UpdateUserRefreshToken(UserRefreshToken token);
        void DeleteUserRefreshToken(UserRefreshToken token);
        Task<UserRefreshToken?> FindUserRefreshToken(string id, bool tracking);
        Task<IEnumerable<UserRefreshToken?>> FindUserRefreshTokensByDeviceId(string deviceId, bool tracking);
        Task<IEnumerable<UserRefreshToken?>> FindUserRefreshTokensByUserIdAndDeviceId(string userid,string deviceId, bool tracking);
        Task<IEnumerable<UserRefreshToken?>> FindUserRefreshTokensByUserId(string userId, bool tracking);


    }
}
