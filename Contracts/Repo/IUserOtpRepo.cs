using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IUserOtpRepo
    {
        Task<UserOtp?> FindUserOtp(string id, bool tracking);
        Task<UserOtp?> FindUserOtpWithPurpose(string id,string purpose, bool tracking);
        Task<UserOtp?> FindUserOtpWithPurposeAndDeviceId(string id, string purpose, string deviceId, bool tracking);


        Task<IEnumerable<UserOtp>> GetAllUserOtps();
        UserOtp CreateUserOtp(UserOtp userOtp);
        void UpdateUserOtp(UserOtp userOtp);
        void DeleteUserOtp(UserOtp userOtp);
    }
}
