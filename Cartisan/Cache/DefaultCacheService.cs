using System;
using System.Collections.Generic;
using Cartisan.Domain;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Cache {
    public class DefaultCacheService : ICacheService {
        private readonly ICache _cache;
        private readonly ICache _localCache;
        private readonly Dictionary<CachingExpirationType, TimeSpan> _cachingExpirationDictionary;

        public bool EnableDistributedCache { get; private set; }

        /// <summary>
        /// 构造函数（仅本机缓存）
        /// </summary>
        /// <param name="cache">本机缓存</param>
        /// <param name="cacheExpirationFactor">缓存过期时间因子</param>
        public DefaultCacheService(ICache cache, float cacheExpirationFactor) :
            this(cache, cache, cacheExpirationFactor, false) {
        }

        public DefaultCacheService(ICache cache, ICache localCache, float cacheExpirationFactor, bool enableDistributedCache) {
            this._cache = cache;
            this._localCache = localCache;
            this.EnableDistributedCache = enableDistributedCache;

            this._cachingExpirationDictionary = new Dictionary<CachingExpirationType, TimeSpan>() {
                { CachingExpirationType.Invariable, new TimeSpan(0, 0, (int)(86400 * cacheExpirationFactor)) }, 
                { CachingExpirationType.Stable, new TimeSpan(0, 0, (int)(28800 * cacheExpirationFactor)) }, 
                { CachingExpirationType.RelativelyStable, new TimeSpan(0, 0, (int)(7200 * cacheExpirationFactor)) }, 
                { CachingExpirationType.UsualSingleObject, new TimeSpan(0, 0, (int)(600 * cacheExpirationFactor)) }, 
                { CachingExpirationType.UsualObjectCollection, new TimeSpan(0, 0, (int)(300 * cacheExpirationFactor)) }, 
                { CachingExpirationType.SingleObject, new TimeSpan(0, 0, (int)(180 * cacheExpirationFactor)) }, 
                { CachingExpirationType.ObjectCollection, new TimeSpan(0, 0, (int)(180 * cacheExpirationFactor)) }
            };
        }

        public void Set(string cacheKey, object value, CachingExpirationType cachingExpirationType) {
            this.Set(cacheKey, value, this._cachingExpirationDictionary[cachingExpirationType]);
        }

        public void Set(string cacheKey, object value, TimeSpan timeSpan) {
            this._cache.Set(cacheKey, value, timeSpan);
        }

        public object Get(string cacheKey) {
            object obj = null;
            if(this.EnableDistributedCache) {
                obj = this._localCache.Get(cacheKey);
            }
            if(obj==null) {
                obj = this._cache.Get(cacheKey);
                if(this.EnableDistributedCache) {
                    this._localCache.Set(cacheKey, obj,
                        this._cachingExpirationDictionary[CachingExpirationType.SingleObject]);
                }
            }
            return obj;
        }

        public T Get<T>(string cacheKey) where T : class {
            return this.Get(cacheKey) as T;
        }

        public object GetFromFirstLevel(string cacheKey) {
            return this._cache.Get(cacheKey);
        }

        public T GetFromFirstLevel<T>(string cacheKey) where T : class {
            return this.GetFromFirstLevel(cacheKey) as T;
        }

        public void Remove(string cacheKey) {
            this._cache.Remove(cacheKey);
        }

        public void Clear() {
            this._cache.Clear();
        }
    }
}