using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    internal static class UserIndexFields
    {
        public const string UserProfileId = "UserProfileId";
        public const string SlugName = "SlugName";

        public const string DisplayName = "DisplayName";
        public const string DisplayNamePrefix = "DisplayName_prefix";
        public const string Company = "Company";
        public const string UserGroupNames = "UserGroupNames";

        public const string IsSeller = "IsSeller";
        public const string City = "City";
        public const string Region = "Region";
        public const string Country = "Country";
        public const string UserStatusId = "UserStatusId";

        public const string SellerProfileId = "SellerProfileId";
        public const string SellerSlugName = "SellerSlugName";
        public const string UserGroupCount = "UserGroupCount";
        public const string ProductCount = "ProductCount";
        public const string TradeCount = "TradeCount";

        public const string CreatedAtTicks = "CreatedAtTicks";
    }
}
