using Contracts.Lucene;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class UserDocumentMapper
    {
        public UserIndexDocument Map(
            UserProfile profile,
            IEnumerable<string> userGroupNames,
            int userGroupCount,
            int productCount,
            int tradeCount)
        {
            if (profile.IdentityUser is null)
                throw new InvalidOperationException(
                    $"UserProfile {profile.UserProfileId} has no IdentityUser loaded.");

            var u = profile.IdentityUser;
            var addr = profile.Address;

            var displayName = string.Join(" ",
                    new[] { u.FirstName, u.LastName }
                        .Where(s => !string.IsNullOrWhiteSpace(s)))
                .Trim();

            // Joined group names — single searchable string with newline separators
            var groupNames = string.Join(" ", userGroupNames ?? Enumerable.Empty<string>());

            return new UserIndexDocument
            {
                UserProfileId = profile.UserProfileId,
                SlugName = profile.Slug,

                DisplayName = displayName,
                Company = profile.Address.Company ?? string.Empty,   // or addr?.Company — pick canonical source
                UserGroupNames = groupNames,

                IsSeller = profile.SellerProfileId.HasValue,
                City = addr?.City ?? string.Empty,
                Region = addr?.Region ?? string.Empty,
                Country = addr?.Country ?? string.Empty,
                UserStatusId = profile.UserStatusId,

                SellerProfileId = profile.SellerProfileId,
                SellerSlugName = profile.SellerProfile?.Slug ?? string.Empty,

                UserGroupCount = userGroupCount,
                ProductCount = productCount,
                TradeCount = tradeCount,

                CreatedAtTicks = profile.CreatedAt.Ticks,
            };
        }
    }
}
