using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.Dtos;
using System.Security.Claims;

namespace saccoshop.ContextFactory
{
    public class CustomClaimsFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        //private readonly IActiveUserProfileService _profileRepo;
        //public CustomClaimsFactory(UserManager<ApplicationUser> userManager,IActiveUserProfileService activeUserProfileRepo, IOptions<IdentityOptions> optionsAccessor)
        //    : base(userManager, optionsAccessor)
        //{
        //    _profileRepo = activeUserProfileRepo;
        //}

        public CustomClaimsFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor)
           : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user )
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim(CustomClaimTypes.FirstName, user.FirstName));
            identity.AddClaim(new Claim(CustomClaimTypes.LastName, user.LastName));

            //var activeUserProfile = await _profileRepo.GetActiveUserProfileAsync(user.Id);
            //if (activeUserProfile.ActiveGroupId != null)
            //{
            //    identity.AddClaim(new Claim(CustomClaimTypes.ActiveGroupId,
            //        activeUserProfile.ActiveGroupId.ToString()
            //    ));
            //}

            var roles = await UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }
    }

}
