using Contracts.Lucene;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public sealed class UserGroupDocumentMapper
    {
        public UserGroupIndexDocument Map(
            UserGroup group,
            int memberCount)
        {
            // The group MUST be loaded with includes for:
            //   .Include(g => g.GroupType)
            //   .Include(g => g.Address)

            var addr = group.Address;
            var groupTypeName = group.GroupType?.Description ?? string.Empty;
            // ↑ adjust to match your nav property and field names

            return new UserGroupIndexDocument
            {
                UserGroupId = group.UserGroupId,
                SlugName = group.Slug, // or group.UserGroupSlugName — match your entity
                UserGroupName = group.UserGroupName,
                AboutGroup = group.AboutGroup ?? string.Empty,
                GroupType = groupTypeName,
                Company = addr?.Company ?? string.Empty,

                GroupTypeId = group.GroupTypeId,
                City = addr?.City ?? string.Empty,
                Region = addr?.Region ?? string.Empty,
                Country = addr?.Country ?? string.Empty,

                MemberCount = memberCount,

                Email = group.Email ?? string.Empty,
                Contact = group.Contact ?? string.Empty,

                CreatedAtTicks = group.CreatedAt.Ticks,
            };
        }
    }
}
