using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    public sealed class LuceneIndexRegistry : ILuceneIndexRegistry, IDisposable
    {
        private readonly Dictionary<string, LuceneIndexContext> _contexts;

        public LuceneIndexRegistry(IOptions<LuceneOptions> options)
        {
            var opts = options.Value;

            if (string.IsNullOrWhiteSpace(opts.RootPath))
                throw new InvalidOperationException(
                    "Lucene:RootPath is not configured.");

            // Register the indexes the app cares about.
            // To add a new searchable entity later (e.g., users), add one line here.
            _contexts = new Dictionary<string, LuceneIndexContext>(StringComparer.OrdinalIgnoreCase)
            {
                ["products"] = new LuceneIndexContext(
                    name: "products",
                    indexPath: Path.Combine(opts.RootPath, "products"),
                    ramBufferMb: opts.RamBufferSizeMb),
                // ["users"] = new LuceneIndexContext(... )  ← future
            };
        }

        public LuceneIndexContext Get(string name)
        {
            if (!_contexts.TryGetValue(name, out var ctx))
                throw new InvalidOperationException(
                    $"Lucene index '{name}' is not registered.");
            return ctx;
        }

        public IReadOnlyCollection<LuceneIndexContext> All => _contexts.Values;

        public void Dispose()
        {
            foreach (var ctx in _contexts.Values) ctx.Dispose();
            _contexts.Clear();
        }
    }
}
