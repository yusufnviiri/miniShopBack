using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    public interface ILuceneIndexRegistry
    {
        /// <summary>
        /// Returns the context for a named index. Throws if the index is not registered.
        /// Known names today: "products". Future: "users", etc.
        /// </summary>
        LuceneIndexContext Get(string name);

        /// <summary>
        /// Returns all registered contexts (used by the periodic refresh service).
        /// </summary>
        IReadOnlyCollection<LuceneIndexContext> All { get; }
    }
}
