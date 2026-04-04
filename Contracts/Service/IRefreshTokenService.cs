using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IRefreshTokenService
    {

        Task<IEnumerable<UserRefreshToken>> GetAllUserRefreshTokensAsync();
        Task< UserRefreshToken> CreateUserRefreshTokenAsync(UserRefreshToken token);
        Task UpdateUserRefreshTokenAsync(UserRefreshToken token);
        Task DeleteUserRefreshTokenAsync(string tokenId);
        Task<UserRefreshToken?> FindUserRefreshTokenAsync(string userId, bool tracking);
        Task<IEnumerable<UserRefreshToken?>> FindUserRefreshTokensByUserIdAsync(string userId, bool tracking);


    }
}
