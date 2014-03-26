namespace Cartisan.Event {
    /// <summary>
    /// 事件发布
    /// </summary>
    public interface IEventPublisher {
        void Publish(params IEvent[] events);
    }
}