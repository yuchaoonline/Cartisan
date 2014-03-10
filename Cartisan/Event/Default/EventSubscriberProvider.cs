using Cartisan.Message.Default;

namespace Cartisan.Event.Default {
    public class EventSubscriberProvider: HandlerProvider<IEventSubscriber<IEvent>>, IEventSubscriberProvider {
        public EventSubscriberProvider(params string[] assemblies): base(assemblies) {}
    }
}