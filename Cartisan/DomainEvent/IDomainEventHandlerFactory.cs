using System.Collections.Generic;

namespace Cartisan.DomainEvent {
    /// <summary>
    /// 领域事件处理器工厂
    /// </summary>
    public interface IDomainEventHandlerFactory {
        /// <summary>
        /// 返回所有响应指定实体的领域事件的处理器
        /// </summary>
        /// <typeparam name="T">领域事件的类型</typeparam>
        /// <param name="domainEvent">具体的领域事件</param>
        /// <returns></returns>
        IEnumerable<IDomainEventHandler<T>> GetDomainEventHandlersFor<T>(T domainEvent) 
            where T : IDomainEvent;
    }
}