using System.Collections.Generic;

namespace Cartisan.DomainEvent {
    /// <summary>
    /// �����¼�����������
    /// </summary>
    public interface IDomainEventHandlerFactory {
        /// <summary>
        /// ����������Ӧָ��ʵ��������¼��Ĵ�����
        /// </summary>
        /// <typeparam name="T">�����¼�������</typeparam>
        /// <param name="domainEvent">����������¼�</param>
        /// <returns></returns>
        IEnumerable<IDomainEventHandler<T>> GetDomainEventHandlersFor<T>(T domainEvent) 
            where T : IDomainEvent;
    }
}