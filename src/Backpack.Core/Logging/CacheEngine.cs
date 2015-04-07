using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Backpack.Core.Logging
{
    public class CacheEngine
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;
        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public static T Get<T>(string key) where T : class
        {
            try
            {
                return (T)Cache[key];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="item">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add<T>(string key, T item) where T : class
        {
            Cache.Add(key, item, DateTimeOffset.Now.AddDays(7));
        }
    }
}
