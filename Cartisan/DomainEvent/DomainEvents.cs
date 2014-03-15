namespace Cartisan.DomainEvent {
    /// <summary>
    /// 
    /// </summary>
    public static class DomainEvents {
        /// <summary>
        /// 领域事件处理器工厂
        /// </summary>
        public static IDomainEventHandlerFactory DomainEventHandlerFactory { get; set; }

        /// <summary>
        /// 唤起事件处理器，响应指定领域事件
        /// </summary>
        /// <typeparam name="T">领域事件类型</typeparam>
        /// <param name="domainEvent">具体事件</param>
        public static void Raise<T>(T domainEvent) where T : IDomainEvent {
            //DomainEventHandlerFactory.GetDomainEventHandlersFor(domainEvent).ForEach(h => h.Handle(domainEvent));
        }
    }
}