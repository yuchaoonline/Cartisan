using System;

namespace Cartisan.Cache {
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache {
        /// <summary>
        /// 添加或更新缓存
        /// </summary>
        /// <param name="cacheKey">缓存项标识</param>
        /// <param name="value">缓存项</param>
        /// <param name="timeSpan">缓存失效时间间隔</param>
        void Set(string cacheKey, object value, TimeSpan timeSpan);

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="cacheKey">缓存项标识</param>
        /// <returns>返回cacheKey对应的缓存项，如果不存在则返回null</returns>
        object Get(string cacheKey);

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="cacheKey">缓存项标识</param>
        void Remove(string cacheKey);

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();
    }
}