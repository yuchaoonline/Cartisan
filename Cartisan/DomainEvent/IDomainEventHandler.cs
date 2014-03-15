namespace Cartisan.DomainEvent {
    /// <summary>
    /// �����¼�������
    /// </summary>
    /// <typeparam name="T">�����¼�������</typeparam>
    public interface IDomainEventHandler<T> where T : IDomainEvent {
        /// <summary>
        /// ���������¼�
        /// </summary>
        /// <param name="domainEvent">����������¼�</param>
        void Handle(T domainEvent);
    }
}