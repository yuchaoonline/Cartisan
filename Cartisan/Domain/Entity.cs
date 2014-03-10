namespace Cartisan.Domain {
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class Entity: IEntity {
        /// <summary>
        /// 领域上下文
        /// </summary>
        public object DomainContext { get; set; }
    }
}