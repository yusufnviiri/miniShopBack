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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepositoryManager _repoManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly ISmsSender _smsSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<JwtSettings> _jwtOptions;
        private readonly RoleManager<IdentityRole> _roleManager;

        // ─── Cached JWT signing material (built once per service lifetime) ───
        private readonly SigningCredentials _signingCredentials;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly int _accessTokenMinutes;
        private readonly JwtSecurityTokenHandler _tokenHandler = new();

        public AuthService(
            ILoggerManager logger,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager,
            IRepositoryManager repositoryManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            ISmsSender smsSender,
            IHttpContextAccessor httpContextAccessor,
            IOptions<JwtSettings> jwtOptions,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _repoManager = repositoryManager;
            _claimsFactory = claimsFactory;
            _smsSender = smsSender;
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = jwtOptions;
            _roleManager = roleManager;

            // Build signing material ONCE — these are thread-safe and immutable.
            var jwt = _jwtOptions.Value;
            _jwtIssuer = jwt.ValidIssuer;
            _jwtAudience = jwt.ValidAudience;
            _accessTokenMinutes = jwt.AccessTokenMinutes;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
            _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        // ───────────────────────────────────────────────────────────────────
        // Helpers
        // ───────────────────────────────────────────────────────────────────

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var principal = await _claimsFactory.CreateAsync(user);
            return principal.Claims.ToList();
        }

        public async Task<string> GetIdentityUserId(string contact)
        {
            var user = await _userManager.FindByNameAsync(contact);
            return user?.Id ?? string.Empty;
        }

        public async Task<string> GetUserRoleId(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? string.Empty;
        }

        private static string GenerateRefreshToken() =>
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        // ───────────────────────────────────────────────────────────────────
        // Token generation — pure, synchronous, no DB calls
        // ───────────────────────────────────────────────────────────────────

        private string GenerateAccessToken(
            ApplicationUser user,
            IList<string> roles,
            IEnumerable<GroupMemberRolesDto> groupRoles)
        {
            var claims = new List<Claim>(capacity: 6 + roles.Count)
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new("security_stamp", user.SecurityStamp),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName),
                new("group_roles", string.Join(",", groupRoles.Select(g => g.Role)))
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_accessTokenMinutes),
                signingCredentials: _signingCredentials);

            return _tokenHandler.WriteToken(token);
        }

        // Kept for backward compatibility with any external callers.
        // Internally it just gathers the data and delegates to the sync version.
        public async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var profileId = await _repoManager.UserProfileRepo
                .GetUserProfileIdFromIdentityUser(user.Id);
            var groupRoles = await _repoManager.GroupMemberRepo
                .GetAllMemberRoles(profileId);

            return GenerateAccessToken(user, roles, groupRoles);
        }

        // ───────────────────────────────────────────────────────────────────
        // Login
        // ───────────────────────────────────────────────────────────────────

        public async Task<TokenResponse> PasswordLoginAsync(LoginRequestDto request)
        {
            // ✅ No AsNoTracking — SignInManager needs the entity tracked.
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            if (user == null)
                throw new ObjectBadRequestExeption("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user, request.Password, lockoutOnFailure: true);

            if (!result.Succeeded)
                throw new ObjectBadRequestExeption("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var profileId = await _repoManager.UserProfileRepo
                                 .GetUserProfileIdFromIdentityUser(user.Id);
            var groupRoles = await _repoManager.GroupMemberRepo
                                 .GetAllMemberRoles(profileId);

            var accessToken = GenerateAccessToken(user, roles, groupRoles);
            var refreshToken = await IssueRefreshTokenAsync(user);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }


        // ───────────────────────────────────────────────────────────────────
        // Refresh tokens
        // ───────────────────────────────────────────────────────────────────

        public async Task<string> IssueRefreshTokenAsync(ApplicationUser user)
        {
            var refreshToken = GenerateRefreshToken();

            var tokenEntity = new UserRefreshToken
            {
                UserId = user.Id,
                DeviceId = null,
                TokenHash = GenerateOtp.HashRefreshToken(refreshToken),
                ExpiresAt = DateTime.UtcNow.AddDays(60)
            };

            // Delete any existing tokens for this user, then add the new one,
            // and persist everything in a SINGLE SaveChanges call.
            var existing = await _repoManager.RefreshTokenRepo
                .FindUserRefreshTokensByUserId(user.Id, true);

            if (existing != null)
            {
                foreach (var t in existing)
                    _repoManager.RefreshTokenRepo.DeleteUserRefreshToken(t);
            }

            _repoManager.RefreshTokenRepo.CreateUserRefreshToken(tokenEntity);
            await _repoManager.SaveRepoDataAsync();

            return refreshToken; // raw token ONLY ever returned here
        }

        public async Task<TokenResponse> RefreshAsync(string refreshToken, string identityUserId)
        {
            var tokens = await _repoManager.RefreshTokenRepo
                .FindUserRefreshTokensByUserId(identityUserId, true);

            var tokenEntity = tokens.FirstOrDefault(x =>
                BCrypt.Net.BCrypt.Verify(refreshToken, x.TokenHash));

            if (tokenEntity == null)
                throw new SecurityException("Invalid refresh token");

            tokenEntity.RevokedAt = DateTime.UtcNow;

            var user = await _userManager.FindByIdAsync(tokenEntity.UserId)
                ?? throw new SecurityException("Invalid refresh token");

            // Gather data once, build access token synchronously.
            var roles = await _userManager.GetRolesAsync(user);
            var profileId = await _repoManager.UserProfileRepo
                                 .GetUserProfileIdFromIdentityUser(user.Id);
            var groupRoles = await _repoManager.GroupMemberRepo
                                 .GetAllMemberRoles(profileId);

            var newAccessToken = GenerateAccessToken(user, roles, groupRoles);
            var newRefreshToken = await IssueRefreshTokenAsync(user);

            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task LogoutAsync(string userId)
        {
            var tokens = await _repoManager.RefreshTokenRepo
                .FindUserRefreshTokensByUserId(userId, true);

            foreach (var t in tokens)
                t.RevokedAt = DateTime.UtcNow;

            await _repoManager.SaveRepoDataAsync();
        }

        // ───────────────────────────────────────────────────────────────────
        // OTP / device flow
        // ───────────────────────────────────────────────────────────────────

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
            await _smsSender.SendAsync(
                user!.PhoneNumber,
                $"Your login code is {otp}. Expires in 2 minutes.");
        }

        public async Task CompleteLoginWithOtpAsync(VerifyLoginOtpRequest request)
        {
            var otpRecord = await _repoManager.UserOtpRepo
                .FindUserOtpWithPurposeAndDeviceId(
                    request.UserId, "Login", request.DeviceId, true);

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

            var device = await _repoManager.UserDeviceRepo
                .FindUserDevice(request.DeviceId, request.UserId, true);

            if (device != null)
            {
                device.IsTrusted = true;
                device.LastSeenAt = DateTime.UtcNow;
            }

            // Single save for both OTP + device updates.
            await _repoManager.SaveRepoDataAsync();
        }

        // ───────────────────────────────────────────────────────────────────
        // Group switching
        // ───────────────────────────────────────────────────────────────────

        public async Task SwitchActiveGroupAsync(Guid userProfileId, Guid groupId)
        {
            if (!await _repoManager.UserGroupRepo.IsGroupMember(userProfileId, groupId))
                throw new ItemNotFoundException(groupId);

            var userProfile = await _repoManager.UserProfileRepo
                .FindUserProfileById(userProfileId, true)
                ?? throw new ItemNotFoundException(userProfileId);

            if (userProfile.ActiveGroupId == groupId)
                return;

            userProfile.ActiveGroupId = groupId;
            await _repoManager.SaveRepoDataAsync();

            await _signInManager.RefreshSignInAsync(userProfile.IdentityUser);
        }

        // ───────────────────────────────────────────────────────────────────
        // Account management
        // ───────────────────────────────────────────────────────────────────

        public async Task ConfirmUserAcount(LoginDataUpdateDto dataUpdateDto)
        {
            var user = await _userManager.FindByNameAsync(dataUpdateDto.PhoneNumber)
                ?? throw new ObjectBadRequestExeption("User does not exist");

            var checkResult = await _signInManager.CheckPasswordSignInAsync(
                user, dataUpdateDto.OldPassword, false);

            if (!checkResult.Succeeded)
                throw new ObjectBadRequestExeption("Invalid credentials");

            var changeResult = await _userManager.ChangePasswordAsync(
                user, dataUpdateDto.OldPassword, dataUpdateDto.NewPassword);

            if (!changeResult.Succeeded)
                throw new Exception(string.Join(", ", changeResult.Errors.Select(e => e.Description)));

            if (!string.IsNullOrEmpty(dataUpdateDto.RecoveryPhoneNumber))
                user.RecoveryPhoneNumber = dataUpdateDto.RecoveryPhoneNumber;

            if (!string.IsNullOrEmpty(dataUpdateDto.RecoveryQuestion) &&
                !string.IsNullOrEmpty(dataUpdateDto.RecoveryAnswer))
            {
                user.RecoveryQuestion = dataUpdateDto.RecoveryQuestion;
                user.RecoveryAnswer = dataUpdateDto.RecoveryAnswer;
                user.AccountConfirmed = true;
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
        }

        public async Task ResetPasswordAsync(PasswordRecoveryDto passwordRecovery)
        {
            var user = await _userManager.FindByNameAsync(passwordRecovery.PhoneNumber)
                ?? throw new ObjectBadRequestExeption("User does not exist");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, passwordRecovery.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task ForceUpdatePasswordAsync(PasswordRecoveryDto passwordRecovery)
        {
            var user = await _userManager.FindByNameAsync(passwordRecovery.PhoneNumber)
                ?? throw new ObjectBadRequestExeption("User does not exist");

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, passwordRecovery.Password);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // ───────────────────────────────────────────────────────────────────
        // Token validation (refresh / expired token handling)
        // ───────────────────────────────────────────────────────────────────

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                ValidIssuer = _jwtIssuer,
                ValidAudience = _jwtAudience,
                IssuerSigningKey = _signingCredentials.Key   // reuse cached key
            };

            var principal = _tokenHandler.ValidateToken(
                token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtToken ||
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