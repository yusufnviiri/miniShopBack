using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public sealed class CachedRepoHelper
    {
        private readonly IMemoryCache _cache;
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

        public CachedRepoHelper(IMemoryCache cache) => _cache = cache;

        public async Task<T?> GetOrCreateAsync<T>(
            string key,
            Func<Task<T?>> factory,
            TimeSpan absolute,
            TimeSpan? sliding = null) where T : class
        {
            if (_cache.TryGetValue(key, out T? hit)) return hit;

            var gate = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
            await gate.WaitAsync();
            try
            {
                if (_cache.TryGetValue(key, out hit)) return hit;     // double-check

                var value = await factory();
                if (value is not null)
                {
                    var opts = new MemoryCacheEntryOptions().SetAbsoluteExpiration(absolute);
                    if (sliding.HasValue) opts.SetSlidingExpiration(sliding.Value);
                    _cache.Set(key, value, opts);
                }
                return value;
            }
            finally
            {
                gate.Release();
                // optional: clean up the gate when nothing else is waiting
            }
        }

        public void Evict(string key) => _cache.Remove(key);
    }
}
