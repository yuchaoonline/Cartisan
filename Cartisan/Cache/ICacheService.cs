using System;

namespace Cartisan.Cache {
    /// <summary>
    /// 缓存服务接口
    /// </summary>
    public interface ICacheService {
        /// <summary>
        /// 是否启用分布式缓存
        /// </summary>
        bool EnableDistributedCache { get; }

        /// <summary>
        /// 添加或更新缓存
        /// </summary>
        /// <param name="cacheKey">缓存项标识</param>
        /// <param name="value">缓存项</param>
        /// <param name="cachingExpirationType">缓存期限类型</param>
        void Set(string cacheKey, object value, CachingExpirationType cachingExpirationType);

        /// <summary>
        /// 添加或更新缓存
        /// </summary>
        /// <param name="cacheKey">缓存项标识</param>
        /// <param name="value">缓存项</param>
        /// <param name="timeSpan">缓存失效时间</param>
        void Set(string cacheKey, object value, TimeSpan timeSpan);

        /// <summary>
        /// 从缓存获取
        /// </summary>
        /// <param name="cacheKey">缓存项标识</param>
        /// <returns>返回cacheKey对应的缓存项，如果不存在则返回null</returns>
        object Get(string cacheKey);

        /// <summary>
        /// 从缓存获取（缓存项必须是引用类型）
        /// </summary>
        /// <typeparam name="T">缓存项类型</typeparam>
        /// <param name="cacheKey">缓存项标识</param>
        /// <returns>返回cacheKey对应的缓存项，如果不存在则返回null</returns>
        T Get<T>(string cacheKey) where T : class;

        /// <summary>
        /// 从一级缓存获取
        /// </summary>
        /// <remarks>在启用分布式缓存的情况下，指穿透二级缓存从一级缓存（分布式缓存）读取</remarks>
        /// <param name="cacheKey">缓存项标识</param>
        /// <returns>返回cacheKey对应的缓存项，如果不存在则返回null</returns>
        object GetFromFirstLevel(string cacheKey);

        /// <summary>
        /// 从一级缓存获取
        /// </summary>
        /// <remarks>在启用分布式缓存的情况下，指穿透二级缓存从一级缓存（分布式缓存）读取</remarks>
        /// <typeparam name="T">缓存项类型</typeparam>
        /// <param name="cacheKey">缓存项标识</param>
        /// <returns>返回cacheKey对应的缓存项，如果不存在则返回null</returns>
        T GetFromFirstLevel<T>(string cacheKey) where T: class;

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cacheKey">缓存项标识</param>
        void Remove(string cacheKey);

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();
    }
}