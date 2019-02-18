using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using in_memory_cache.Models;
using Microsoft.Extensions.Caching.Memory;

namespace in_memory_cache.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache _cache;

        public HomeController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public IActionResult Index()
        {
            return View("Cache");
        }

        public IActionResult CacheTryGetValueSet()
        {
            DateTime cacheEntry;

            // Look for cache key.
            if (!_cache.TryGetValue(CacheKeys.Entry, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = DateTime.Now;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.NeverRemove)
                    // Keep in cache for this time, reset time if accessed.
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

                // Save data in cache.
                _cache.Set(CacheKeys.Entry, cacheEntry, cacheEntryOptions);
            }

            return View("Cache", cacheEntry);
        }

        public IActionResult CacheGetOrCreate()
        {
            var cacheEntry = _cache.GetOrCreate(CacheKeys.Entry, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(3);
                return DateTime.Now;
            });

            return View("Cache", cacheEntry);
        }

        public async Task<IActionResult> CacheGetOrCreateAsync()
        {
            var cacheEntry = await
                _cache.GetOrCreateAsync(CacheKeys.Entry, entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromSeconds(3);
                    return Task.FromResult(DateTime.Now);
                });

            return View("Cache", cacheEntry);
        }

        public IActionResult CacheGet()
        {
            var cacheEntry = _cache.Get<DateTime?>(CacheKeys.Entry);
            return View("Cache", cacheEntry);
        }

        public IActionResult CacheRemove()
        {
            DateTime cacheEntry;

            // Look for cache key.
            if (_cache.TryGetValue(CacheKeys.Entry, out cacheEntry))
            {
                _cache.Remove(CacheKeys.Entry);
            }

            return View("Cache", null);
        }
    }

    public static class CacheKeys
    {
        public static string Entry { get { return "_CacheEntry"; } }
    }
}
