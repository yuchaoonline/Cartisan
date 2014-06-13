using System;
using System.Linq;
using System.Runtime.Caching;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Cache {
    /// <summary>
    /// 使用System.Runtime.Caching.MemoryCache实现的本机缓存
    /// </summary>
    /// <remarks>仅能在.net framework4.0及以上版本使用</remarks>
    public class RuntimeMemoryCache: ICache {
        private readonly MemoryCache _cache = MemoryCache.Default;

        public void Set(string cacheKey, object value, TimeSpan timeSpan) {
            if(string.IsNullOrEmpty(cacheKey) || value==null) {
                return;
            }
            this._cache.Set(cacheKey, value, DateTimeOffset.Now.Add(timeSpan));
        }

        public object Get(string cacheKey) {
            return this._cache[cacheKey];
        }

        public void Remove(string cacheKey) {
            this._cache.Remove(cacheKey);
        }

        public void Clear() {
            this._cache.Select(c=>c.Key).ForEach(key=>this._cache.Remove(key));
        }
    }
}