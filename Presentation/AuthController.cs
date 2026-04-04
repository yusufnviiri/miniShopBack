using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Services.Redis;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{

        [Route("api/auth")]
        [ApiController]
        public class AuthController : ControllerBase
        {

            private readonly IServiceManager _service;
            private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<ApplicationUser>_roleManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMemoryCache _cache;



        public AuthController(IServiceManager service, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMemoryCache cache)
            {
                _service = service;
                _userManager = userManager;
                _signInManager = signInManager;
              _cache = cache;

            //_roleManager = roleManager;
        }
        private async Task<ApplicationUser?> GetCurrentUserDetails()
        {
            var userName = User.Identity?.Name; // 

            var userIdentity = User.Identity;
            if (!userIdentity.IsAuthenticated && userName is null)
            {
                return null;
            }
            else
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null) {return null; }
                return user;
            }
        }



        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _service.AuthService.PasswordLoginAsync(request);
        //    //return Ok(result);
        //    return Ok(new { message = result });
        //}
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            _cache.Remove("cachedUser");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _service.AuthService.PasswordLoginAsync(request);
            if (token == null)
                {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(token);
        
        }



        [HttpPost("logout")]

            public async Task<ActionResult> LogOutUser()
            {
            _cache.Remove("cachedUser");

            var userName = User.Identity?.Name; // 
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user is not null)
                {
                    await _service.AuthService.LogoutAsync(user.Id);
                }
                return Ok(new {message= "Accepted"});
                }
            return Ok(new { message = "Accepted" });

        }




        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh( [FromBody] RefreshRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new {message="Invalid Object Data"});
            var principal = _service.AuthService.GetPrincipalFromExpiredToken(request.AccessToken);
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier).Value; if (userId == null||userId==string.Empty) return BadRequest(new { message = "Invalid User Credentials" });


            //var result = await _service.AuthService.RefreshAsync( request.RefreshToken,  request.DeviceId);


            var result = await _service.AuthService.RefreshAsync(request.RefreshToken, userId);
             _cache.Remove("cachedUser");


            return Ok(result);

        }

        //[HttpPost("verifyphone")]
        //public async Task<IActionResult> VerifyPhone([FromBody] VerifyOtpRequest request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    await _service.UserProfileService.VerifyPhoneAsync(request);
        //    return Ok();
        //}

        [Authorize]

        [HttpPost("confirmaccount")]
        public async Task<IActionResult> ConfirmUserAcount([FromBody] LoginDataUpdateDto loginDataUpdate )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AuthService.ConfirmUserAcount(loginDataUpdate);
            //return Ok(result);
            return Ok(new { message = "Updated" });

        }
        [HttpPost("recoverpassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] PasswordRecoveryDto recoveryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AuthService.ResetPasswordAsync(recoveryDto);
            //return Ok(result);
            return Ok(new { message = "Updated" });

        }
        [HttpPost("forcepasswordreset")]
        public async Task<IActionResult> ForceUpdatePasswordAsync([FromBody] PasswordRecoveryDto recoveryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AuthService.ForceUpdatePasswordAsync(recoveryDto);
            //return Ok(result);
            return Ok(new { message = "Updated" });

        }

        [HttpGet("myprofile")]
        public async Task<IActionResult> MyProfile()
        {
            LoggedInUserDataDto loggedInUser = new();
            var cacheKey = "cachedUser";
            if (_cache.TryGetValue(cacheKey, out LoggedInUserDataDto usercache))
            {
                if (usercache != null)
                {
                    loggedInUser = usercache;
                }
            }
            else
            {
                var user = await GetCurrentUserDetails();
                //if (userId == null) return BadRequest(new {message="You Need to logIn"});
                if (user == null) return Unauthorized(new { message = "You Need to logIn" });
                var userRole = await _service.AuthService.GetUserRoleId(user);
                var profileId = await _service.UserProfileService.GetUserProfileIdFromIdentityUserAsync(user.Id);
                if (profileId == Guid.Empty) return Ok(new { message = "User profile not found" });
                var userProfile = await _service.UserProfileService.GetLoggedInUserDataDtoAsync(profileId);
                userProfile.IdentityRole = userRole;
                var cacheOptions = new MemoryCacheEntryOptions()
                           .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)) // hard expiry
                           .SetSlidingExpiration(TimeSpan.FromMinutes(2)); // refresh if used

                _cache.Set(cacheKey, userProfile, cacheOptions);
                loggedInUser = userProfile;
            }
            return Ok(loggedInUser);


        }

        [HttpGet("me")]
        public  async Task<IActionResult> Me()
        {
            LoggedInUserDataDto loggedInUser = new ();
            var cacheKey = "cachedUser";
            if (_cache.TryGetValue(cacheKey, out LoggedInUserDataDto usercache))
            {
                if (usercache != null)
                {
                    loggedInUser = usercache;                }
            }
            else
            {
                var user = await GetCurrentUserDetails();
                //if (userId == null) return BadRequest(new {message="You Need to logIn"});
                if (user == null) return Ok(new LoggedInUserDataDto());
                var userRole = await _service.AuthService.GetUserRoleId(user);
                var profileId = await _service.UserProfileService.GetUserProfileIdFromIdentityUserAsync(user.Id);
                if (profileId == Guid.Empty) return Ok(new { message = "User profile not found" });
                var userProfile = await _service.UserProfileService.GetLoggedInUserDataDtoAsync(profileId);
                userProfile.IdentityRole = userRole;
                var cacheOptions = new MemoryCacheEntryOptions()
                           .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)) // hard expiry
                           .SetSlidingExpiration(TimeSpan.FromMinutes(2)); // refresh if used

                _cache.Set(cacheKey, userProfile, cacheOptions);
                loggedInUser = userProfile;
            }
            return Ok(loggedInUser);


        }

        [Authorize]
        [HttpGet("confirmLogin")]
        public  IActionResult ConfirmLogin()
        {
            var userName = User.Identity?.Name; // 
            if(userName == null) return Unauthorized(new { message = "Invalid credentials" });

            return Ok(new {message="confirmed"});
            

        }

    }
}

