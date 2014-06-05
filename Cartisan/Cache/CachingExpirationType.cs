namespace Cartisan.Cache {
    /// <summary>
    /// 缓存期限类型
    /// </summary>
    public enum CachingExpirationType {
        /// <summary>
        /// 永久不变的
        /// </summary>
        Invariable,
        /// <summary>
        /// 稳定数据
        /// </summary>
        Stable,
        /// <summary>
        /// 相对稳定
        /// </summary>
        RelativelyStable,
        /// <summary>
        /// 常用的单个对象
        /// </summary>
        UsualSingleObject,
        /// <summary>
        /// 常用的对象集合
        /// </summary>
        UsualObjectCollection,
        /// <summary>
        /// 单个对象
        /// </summary>
        SingleObject,
        /// <summary>
        /// 对象集合
        /// </summary>
        ObjectCollection
    }
}