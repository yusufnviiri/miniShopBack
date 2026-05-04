using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    public sealed class LuceneOptions
    {
        public const string SectionName = "Lucene";

        /// <summary>
        /// Root folder where all Lucene indexes live. Each named index
        /// gets its own subfolder (e.g., {RootPath}/products, {RootPath}/users).
        /// </summary>
        public string RootPath { get; set; } = string.Empty;

        /// <summary>
        /// IndexWriter RAM buffer in MB before flushing to disk.
        /// 32 is a good default; raise for bulk re-indexing, lower for tight RAM.
        /// </summary>
        public double RamBufferSizeMb { get; set; } = 32;

        /// <summary>
        /// How often the SearcherManager refreshes the searcher to see new writes.
        /// Lower = fresher results, more CPU. 2-5 seconds is typical.
        /// </summary>
        public int SearcherRefreshSeconds { get; set; } = 3;

        /// <summary>
        /// Hard cap on results per page, regardless of what the client asks for.
        /// Defends against memory blowup from huge page sizes.
        /// </summary>
        public int MaxPageSize { get; set; } = 50;
    }
}
