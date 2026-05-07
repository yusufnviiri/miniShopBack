using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lucene
{
    public sealed class UserSearchRequest
    {
        public string? Query { get; set; }

        public bool? IsSeller { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }

        public UserSortMode Sort { get; set; } = UserSortMode.Relevance;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public enum UserSortMode
    {
        Relevance = 0,
        NewestFirst = 1,
        MostActive = 2, // ProductCount + TradeCount weighted
    }
}
