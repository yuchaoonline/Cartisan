namespace Cartisan.Event {
    /// <summary>
    /// 事件订阅
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventSubscriber<in TEvent> 
        where TEvent: class, IEvent {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="evnet"></param>
        void Handle(TEvent evnet);
    }
}