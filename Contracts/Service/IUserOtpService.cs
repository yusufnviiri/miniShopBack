using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IUserOtpService
    {
        Task<UserOtp?> FindUserOtpAsync(string id, bool tracking);
        Task<IEnumerable<UserOtp>> GetAllUserOtpsAsync();
        Task CreateUserOtpAsyncRef(ApplicationUser user);
        Task CreateUserOtpAsync(UserOtp userOtp );

        Task UpdateUserOtpAsync(UserOtp userOtp);
        Task DeleteUserOtpAsync(string userOtp);
    }
}
