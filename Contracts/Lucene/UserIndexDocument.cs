using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public sealed class UserIndexDocument
    {
        // Identity
        public Guid UserProfileId { get; set; }
        public string SlugName { get; set; } = string.Empty;

        // Searchable text
        public string DisplayName { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string UserGroupNames { get; set; } = string.Empty;

        // Filterable (exact match)
        public bool IsSeller { get; set; }
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int UserStatusId { get; set; }

        // Display extras (stored only)
        public Guid? SellerProfileId { get; set; }
        public string SellerSlugName { get; set; } = string.Empty;
        public int UserGroupCount { get; set; }
        public int ProductCount { get; set; }
        public int TradeCount { get; set; }

        // Sort
        public long CreatedAtTicks { get; set; }
    }
}

