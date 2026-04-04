using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class GenerateOtp
    {
        public static string GenerateOtpEndPoint()
        {
            var bytes = RandomNumberGenerator.GetBytes(4);
            var code = BitConverter.ToUInt32(bytes, 0) % 1_000_000;
            return code.ToString("D6");
        }
        public static string HashOtp(string otp)
        {
            return BCrypt.Net.BCrypt.HashPassword(otp);
        }


        public static string HashIp(string deviceIp)
        {
            return BCrypt.Net.BCrypt.HashPassword(deviceIp);
        }
        public static string HashRefreshToken(string token)
        {
            return BCrypt.Net.BCrypt.HashPassword(token);
        }



    }

}
