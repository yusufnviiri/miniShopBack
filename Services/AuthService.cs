using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos;
using SixLabors.ImageSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    internal sealed class AuthService : IAuthService
    {
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepositoryManager _repoManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly ISmsSender _smsSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<JwtSettings> _jwtOptions;
        private readonly RoleManager<IdentityRole> _roleManager;



        public AuthService(ILoggerManager logger, UserManager<ApplicationUser> userManager, IConfiguration configuration, SignInManager<ApplicationUser> signInMager, IRepositoryManager repositoryManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, ISmsSender smsSender, IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtOptions, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInMager;
            _repoManager = repositoryManager;
            _claimsFactory = claimsFactory;
            _smsSender = smsSender;
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = jwtOptions;
            _roleManager = roleManager;
        }
        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var principal = await _claimsFactory.CreateAsync(user);
            return principal.Claims.ToList();
        }

        public async Task<string> GetIdentityUserId(string contact)
        {
            _user = await _userManager.FindByNameAsync(contact);
            if (_user is not null)
            {
                return _user.Id;
            }
            else return string.Empty;


        }

        public async Task<string> GetUserRoleId(ApplicationUser user)
        {
            var userRole = await _userManager.GetRolesAsync(user);
                return userRole.ToList().FirstOrDefault()??string.Empty;       
                                   
        }



        public async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
        {
            //public async Task<string> GenerateAccessTokenAsync(ApplicationUser user,string deviceId)
            //{

            var jwtSettings = _jwtOptions.Value;
            var roles = await _userManager.GetRolesAsync(user);
            //        var claims = new List<Claim>
            //{
            //    new(JwtRegisteredClaimNames.Sub, user.Id), new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new("security_stamp", user.SecurityStamp),new("device_id", deviceId),new(ClaimTypes.NameIdentifier, user.Id)
            //};

            var claims = new List<Claim>
{
    new(JwtRegisteredClaimNames.Sub, user.Id),
    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    new("security_stamp", user.SecurityStamp),

    new(ClaimTypes.NameIdentifier, user.Id),
    new(ClaimTypes.Name, user.UserName)
};

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            // Group roles (example)
            Guid userProfileId = await _repoManager.UserProfileRepo.GetUserProfileIdFromIdentityUser(user.Id);
            var groupRoles = await _repoManager.GroupMemberRepo.GetAllMemberRoles(userProfileId);


            claims.Add(new Claim("group_roles",
                string.Join(",", groupRoles)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.ValidIssuer,
                audience: jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }














































        public async Task SwitchActiveGroupAsync(Guid userProfileId, Guid groupId)
        {
            if (!await _repoManager.UserGroupRepo.IsGroupMember(userProfileId, groupId))
                throw new ItemNotFoundException(groupId);

            var userProfile = await _repoManager.UserProfileRepo
                .FindUserProfileById(userProfileId, true)
                ?? throw new ItemNotFoundException(userProfileId);

            if (userProfile.ActiveGroupId == groupId)
                return; // no-op

            userProfile.ActiveGroupId = groupId;
            await _repoManager.SaveRepoDataAsync();

            await _signInManager.RefreshSignInAsync(userProfile.IdentityUser);
        }

        public async Task<TokenResponse> PasswordLoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            if (user == null)
                throw new ObjectBadRequestExeption("Invalid credentials");

            //if (!user.PhoneNumberVerified)
            //throw new ObjectBadRequestExeption("Phone number not verified");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true);

            if (!result.Succeeded)
                throw new ObjectBadRequestExeption("Invalid credentials");

            var newAccessToken = await GenerateAccessTokenAsync(user!);
            //await _signInManager.SignInAsync(user!, isPersistent: false);
            var refreshToken = await IssueRefreshTokenAsync(user!);
                return new TokenResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = refreshToken
                };


            //return new LoginStageResult
            //{
            //    UserId = user.Id,
            //    RequiresOtp = false // ALWAYS true for now
            //};
        }

        public async Task IssueLoginOtpAsync(string userId, string deviceId)
        {
            var otp = GenerateOtp.GenerateOtpEndPoint();

            var otpEntity = new UserOtp
            {
                UserId = userId,
                CodeHash = GenerateOtp.HashOtp(otp),
                Purpose = "Login",
                DeviceId = deviceId,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                AttemptCount = 0,
                Used = false
            };
            _repoManager.UserOtpRepo.CreateUserOtp(otpEntity);
            await _repoManager.SaveRepoDataAsync();

            var user = await _userManager.FindByIdAsync(userId);

            await _smsSender.SendAsync(user!.PhoneNumber, $"Your login code is {otp}. Expires in 2 minutes."
            );
        }


        public async Task CompleteLoginWithOtpAsync(VerifyLoginOtpRequest request)
        {


            var otpRecord = await _repoManager.UserOtpRepo.FindUserOtpWithPurposeAndDeviceId(request.UserId, "Login", request.DeviceId, false);


            if (otpRecord == null)
                throw new ObjectBadRequestExeption("OTP expired or invalid");

            if (otpRecord.AttemptCount >= 5)
                throw new ObjectBadRequestExeption("Too many attempts");

            otpRecord.AttemptCount++;

            if (!BCrypt.Net.BCrypt.Verify(request.Code, otpRecord.CodeHash))
            {
                await _repoManager.SaveRepoDataAsync();
                throw new ObjectBadRequestExeption("Invalid code");
            }

            otpRecord.Used = true;



            var device = await _repoManager.UserDeviceRepo.FindUserDevice(request.DeviceId, request.UserId, true);
            if (device != null)
            {

                device.IsTrusted = true;
                device.LastSeenAt = DateTime.UtcNow;

                await _repoManager.SaveRepoDataAsync();
            }
        }

        public async Task<string> IssueRefreshTokenAsync(ApplicationUser user)
        {

            //public async Task<string> IssueRefreshTokenAsync(ApplicationUser user, string deviceId)
            //{
            var refreshToken = GenerateRefreshToken();

            var tokenEntity = new UserRefreshToken
            {
                UserId = user.Id,
                //DeviceId = deviceId,
                DeviceId = null,

                TokenHash = GenerateOtp.HashRefreshToken(refreshToken),
                ExpiresAt = DateTime.UtcNow.AddDays(60)
            };
            //delete tokens
            var tokens = await _repoManager.RefreshTokenRepo.FindUserRefreshTokensByUserId(user.Id, false);
            if (tokens != null && tokens.Any())
            {
                foreach (var item in tokens)
                {
                    _repoManager.RefreshTokenRepo.DeleteUserRefreshToken(item);

                }
                await _repoManager.SaveRepoDataAsync();
            }
                var newToken = _repoManager.RefreshTokenRepo.CreateUserRefreshToken(tokenEntity);
                await _repoManager.SaveRepoDataAsync();


                return refreshToken; // raw token ONLY sent to client
            
        }


        public async Task<TokenResponse> RefreshAsync(string refreshToken, string identityUserId)
        {
            //public async Task<TokenResponse> RefreshAsync(string refreshToken, string deviceId)
            //{
            var tokens = await _repoManager.RefreshTokenRepo.FindUserRefreshTokensByUserId(identityUserId, false);

            var tokenEntity = tokens.FirstOrDefault(x =>
                BCrypt.Net.BCrypt.Verify(refreshToken, x.TokenHash));

            if (tokenEntity == null)
                throw new SecurityException("Invalid refresh token");

            tokenEntity.RevokedAt = DateTime.UtcNow;

            var user = await _userManager.FindByIdAsync(tokenEntity.UserId);

            //var newAccessToken = await GenerateAccessTokenAsync(user!, deviceId);
            //var newRefreshToken = await IssueRefreshTokenAsync(user!, deviceId);

            var newAccessToken = await GenerateAccessTokenAsync(user!);

            var newRefreshToken = await IssueRefreshTokenAsync(user!);

            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
        public async Task LogoutAsync(string userId)
        {
            //public async Task LogoutAsync(string userId, string deviceId)
            //{
            //var tokens = await _repoManager.RefreshTokenRepo.FindUserRefreshTokenByUserIdAndDeviceId(userId, deviceId, true);
            var tokens = await _repoManager.RefreshTokenRepo.FindUserRefreshTokensByUserId(userId, true);

            foreach (var t in tokens)
                t.RevokedAt = DateTime.UtcNow;

            await _repoManager.SaveRepoDataAsync();
        }
        public async Task ConfirmUserAcount(LoginDataUpdateDto dataUpdateDto)
        {
            _user = await _userManager.FindByNameAsync(dataUpdateDto.PhoneNumber);
            if (_user is not null)
            {
                //update password
                var checkResult = await _signInManager.CheckPasswordSignInAsync(_user, dataUpdateDto.OldPassword, false);
                if (!checkResult.Succeeded)
                    throw new ObjectBadRequestExeption("Invalid credentials");
                var result = await _userManager.ChangePasswordAsync(_user, dataUpdateDto.OldPassword, dataUpdateDto.NewPassword);

                if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                //update recovery data
                if (!string.IsNullOrEmpty(dataUpdateDto.RecoveryPhoneNumber))
                {
                    _user.RecoveryPhoneNumber = dataUpdateDto.RecoveryPhoneNumber;
                }
                if (!string.IsNullOrEmpty(dataUpdateDto.RecoveryQuestion) && !string.IsNullOrEmpty(dataUpdateDto.RecoveryAnswer))
                {
                    _user.RecoveryQuestion = dataUpdateDto.RecoveryQuestion;
                    _user.RecoveryAnswer = dataUpdateDto.RecoveryAnswer;
                    _user.AccountConfirmed=true;
                }
                await _userManager.UpdateAsync(_user);
                await _signInManager.RefreshSignInAsync(_user);

            }
            else
            {
                throw new ObjectBadRequestExeption("User does not exist");

            }

        }

        public async Task ResetPasswordAsync(PasswordRecoveryDto passwordRecovery)
        {
            var user = await _userManager.FindByNameAsync(passwordRecovery.PhoneNumber);

            if (user == null)
                throw new ObjectBadRequestExeption("User does not exist");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, passwordRecovery.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        public async Task ForceUpdatePasswordAsync(PasswordRecoveryDto passwordRecovery)
        {
            var user = await _userManager.FindByNameAsync(passwordRecovery.PhoneNumber);

            if (user == null)
                throw new ObjectBadRequestExeption("User does not exist");
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, passwordRecovery.Password);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSection = _jwtOptions.Value;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.SecretKey));

            if (secretKey==null)
                throw new InvalidOperationException("JWT SecretKey is missing.");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // IMPORTANT
                ValidIssuer = jwtSection.ValidIssuer,
                ValidAudience = jwtSection.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSection.SecretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out SecurityToken securityToken);

            var jwtToken = securityToken as JwtSecurityToken;

            if (jwtToken == null ||
                !jwtToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

    }
}
