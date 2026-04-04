using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
   public interface IAuthService
    {
     
        Task SwitchActiveGroupAsync(Guid userId, Guid groupId);
        Task<string> GetIdentityUserId(string contact);
        Task<string> GetUserRoleId(ApplicationUser user);

        Task<TokenResponse> PasswordLoginAsync(LoginRequestDto request);

        //Task<LoginStageResult> PasswordLoginAsync(LoginRequestDto request);
        Task IssueLoginOtpAsync(string userId, string deviceId);
        Task CompleteLoginWithOtpAsync(VerifyLoginOtpRequest request);
        //Task<string> IssueRefreshTokenAsync(ApplicationUser user, string deviceId);
        //Task LogoutAsync(string userId, string deviceId);
        //Task<TokenResponse> RefreshAsync(string refreshToken, string deviceId);

        Task<string> IssueRefreshTokenAsync(ApplicationUser user);

        Task<TokenResponse> RefreshAsync(string refreshToken, string userId);
        Task LogoutAsync(string userId);
        Task ConfirmUserAcount(LoginDataUpdateDto dataUpdateDto);
        Task ResetPasswordAsync(PasswordRecoveryDto passwordRecovery);
        Task ForceUpdatePasswordAsync(PasswordRecoveryDto passwordRecovery);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);


    }
}
