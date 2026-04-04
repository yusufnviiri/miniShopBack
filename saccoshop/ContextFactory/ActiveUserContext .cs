using Contracts.Service;
using System.Security.Claims;

namespace saccoshop.ContextFactory
{
    public sealed class ActiveUserContext : IActiveUserContext
    {
        public ActiveUserContext(IHttpContextAccessor accessor)
        {
            var user = accessor.HttpContext!.User;
            UserId = Guid.Parse(
                user.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            //var groupClaim = user.FindFirst(CustomClaimTypes.ActiveGroupId);
            //ActiveGroupId = groupClaim != null
            //    ? Guid.Parse(groupClaim.Value)
            //    : null;
        }

        public Guid UserId { get; }
        public Guid? ActiveGroupId { get; }
    }

}
