namespace Cartisan.DomainEvent {
    /// <summary>
    /// 
    /// </summary>
    public static class DomainEvents {
        /// <summary>
        /// �����¼�����������
        /// </summary>
        public static IDomainEventHandlerFactory DomainEventHandlerFactory { get; set; }

        /// <summary>
        /// �����¼�����������Ӧָ�������¼�
        /// </summary>
        /// <typeparam name="T">�����¼�����</typeparam>
        /// <param name="domainEvent">�����¼�</param>
        public static void Raise<T>(T domainEvent) where T : IDomainEvent {
            //DomainEventHandlerFactory.GetDomainEventHandlersFor(domainEvent).ForEach(h => h.Handle(domainEvent));
        }
    }
}