using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lucene
{
    public sealed class UserGroupSearchRequest
    {
        public string? Query { get; set; }

        public int? GroupTypeId { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }

        public int? MinMembers { get; set; }

        public UserGroupSortMode Sort { get; set; } = UserGroupSortMode.Relevance;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public enum UserGroupSortMode
    {
        Relevance = 0,
        NewestFirst = 1,
        MostMembers = 2,
    }
}
