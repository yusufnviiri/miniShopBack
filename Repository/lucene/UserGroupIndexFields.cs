using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    internal static class UserGroupIndexFields
    {
        public const string UserGroupId = "UserGroupId";
        public const string SlugName = "SlugName";

        public const string UserGroupName = "UserGroupName";
        public const string UserGroupNamePrefix = "UserGroupName_prefix";
        public const string AboutGroup = "AboutGroup";
        public const string GroupType = "GroupType";
        public const string Company = "Company";

        public const string GroupTypeId = "GroupTypeId";
        public const string City = "City";
        public const string Region = "Region";
        public const string Country = "Country";

        public const string MemberCount = "MemberCount";

        public const string Email = "Email";
        public const string Contact = "Contact";

        public const string CreatedAtTicks = "CreatedAtTicks";
    }
}
