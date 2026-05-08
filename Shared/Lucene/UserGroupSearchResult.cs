using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lucene
{
    public sealed class UserGroupSearchResult
    {
        public IReadOnlyList<UserGroupCardDto> Items { get; set; } = [];
        public int TotalHits { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages =>
            PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalHits / PageSize);
        public long ElapsedMs { get; set; }
    }
}
