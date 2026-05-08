using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public sealed class UserGroupIndexDocument
    {
        public Guid UserGroupId { get; set; }
        public string SlugName { get; set; } = string.Empty;

        // Searchable text
        public string UserGroupName { get; set; } = string.Empty;
        public string AboutGroup { get; set; } = string.Empty;
        public string GroupType { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;

        // Filterable
        public int GroupTypeId { get; set; }
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        // Display + sort
        public int MemberCount { get; set; }

        // Display-only
        public string Email { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;

        // Sort
        public long CreatedAtTicks { get; set; }
    }
}
