using Lucene.Net.Documents;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    internal static class UserGroupCardProjector
    {
        public static UserGroupCardDto Project(Document doc, float score)
        {
            return new UserGroupCardDto
            {
                UserGroupId = ParseGuidN(doc.Get(UserGroupIndexFields.UserGroupId)),
                SlugName = doc.Get(UserGroupIndexFields.SlugName) ?? string.Empty,

                UserGroupName = doc.Get(UserGroupIndexFields.UserGroupName) ?? string.Empty,
                AboutGroup = doc.Get(UserGroupIndexFields.AboutGroup) ?? string.Empty,
                GroupType = doc.Get(UserGroupIndexFields.GroupType) ?? string.Empty,
                Company = doc.Get(UserGroupIndexFields.Company) ?? string.Empty,

                GroupTypeId = doc.GetField(UserGroupIndexFields.GroupTypeId)?.GetInt32Value() ?? 0,
                City = doc.Get(UserGroupIndexFields.City) ?? string.Empty,
                Region = doc.Get(UserGroupIndexFields.Region) ?? string.Empty,
                Country = doc.Get(UserGroupIndexFields.Country) ?? string.Empty,

                Email = doc.Get(UserGroupIndexFields.Email) ?? string.Empty,
                Contact = doc.Get(UserGroupIndexFields.Contact) ?? string.Empty,

                MemberCount = doc.GetField(UserGroupIndexFields.MemberCount)?.GetInt32Value() ?? 0,

                Score = score,
            };
        }

        private static Guid ParseGuidN(string? s) =>
            string.IsNullOrEmpty(s) ? Guid.Empty : Guid.ParseExact(s, "N");
    }
}
