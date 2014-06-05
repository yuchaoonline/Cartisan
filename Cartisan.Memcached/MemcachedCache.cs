using System;
using System.Web;
using Cartisan.Cache;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace Cartisan.Memcached {
    public class MemcachedCache: ICache {
        private MemcachedClient _cache = new MemcachedClient();

        public void Set(string cacheKey, object value, TimeSpan timeSpan) {
            this._cache.Store(StoreMode.Set, cacheKey, value, DateTime.Now.Add(timeSpan));
        }

        public object Get(string cacheKey) {
            HttpContext current = HttpContext.Current;
            if(current!=null && current.Items.Contains(cacheKey)) {
                return current.Items[cacheKey];
            }
            object obj = this._cache.Get(cacheKey);
            if(current!=null && obj!=null) {
                current.Items[cacheKey] = obj;
            }
            return obj;
        }

        public void Remove(string cacheKey) {
            this._cache.Remove(cacheKey);
        }

        public void Clear() {
            this._cache.FlushAll();
        }
    }
}
