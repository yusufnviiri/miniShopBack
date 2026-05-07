using Lucene.Net.Documents;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    internal static class UserCardProjector
    {
        public static UserCardDto Project(Document doc, float score)
        {
            var sellerProfileIdRaw = doc.Get(UserIndexFields.SellerProfileId);

            return new UserCardDto
            {
                UserProfileId = ParseGuidN(doc.Get(UserIndexFields.UserProfileId)),
                DisplayName = doc.Get(UserIndexFields.DisplayName) ?? string.Empty,
                SlugName = doc.Get(UserIndexFields.SlugName) ?? string.Empty,
                Company = doc.Get(UserIndexFields.Company) ?? string.Empty,
                City = doc.Get(UserIndexFields.City) ?? string.Empty,
                Region = doc.Get(UserIndexFields.Region) ?? string.Empty,
                Country = doc.Get(UserIndexFields.Country) ?? string.Empty,
                IsSeller = doc.Get(UserIndexFields.IsSeller) == "1",
                SellerProfileId = string.IsNullOrEmpty(sellerProfileIdRaw)
                    ? null
                    : Guid.ParseExact(sellerProfileIdRaw, "N"),
                SellerSlugName = doc.Get(UserIndexFields.SellerSlugName) ?? string.Empty,
                UserGroupCount = doc.GetField(UserIndexFields.UserGroupCount)?.GetInt32Value() ?? 0,
                ProductCount = doc.GetField(UserIndexFields.ProductCount)?.GetInt32Value() ?? 0,
                TradeCount = doc.GetField(UserIndexFields.TradeCount)?.GetInt32Value() ?? 0,
                Score = score,
            };
        }

        private static Guid ParseGuidN(string? s) =>
            string.IsNullOrEmpty(s) ? Guid.Empty : Guid.ParseExact(s, "N");
    }
}
