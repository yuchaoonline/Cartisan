namespace Cartisan.DomainEvent {
    /// <summary>
    /// 领域事件处理器
    /// </summary>
    /// <typeparam name="T">领域事件的类型</typeparam>
    public interface IDomainEventHandler<T> where T : IDomainEvent {
        /// <summary>
        /// 处理领域事件
        /// </summary>
        /// <param name="domainEvent">具体的领域事件</param>
        void Handle(T domainEvent);
    }
}