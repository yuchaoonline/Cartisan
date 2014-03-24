/*using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Cartisan.Infrastructure.Extensions;
using Cartisan.Message;

namespace Cartisan.Event.Default {
    public class DomainEventBus: IDomainEventBus {
        protected ConcurrentQueue<IDomainEvent> DomainEventQueue { get; set; }
        protected IEventSubscriberProvider EventSubscriberProvider { get; set; }
        protected IEventPublisher EventPublisher { get; set; }
        protected IEnumerable<IMessageContext> SentMessageContexts { get; set; }

        public DomainEventBus(IEventSubscriberProvider provider, IEventPublisher eventPublisher) {
            this.EventSubscriberProvider = provider;
            this.EventPublisher = eventPublisher;
            DomainEventQueue = new ConcurrentQueue<IDomainEvent>();
        }

        public void Dispose() {
        }

        public void Commit() {
            SentMessageContexts = EventPublisher.Publish(this.DomainEventQueue.ToArray());
        }

        public void Publish<TEvent>(TEvent @event) where TEvent: IDomainEvent {
            DomainEventQueue.Enqueue(@event);
            IList<object> eventSubscribers = EventSubscriberProvider.GetHandlers(@event.GetType());
            eventSubscribers.ForEach(eventSubscriber => ((dynamic)eventSubscriber).Handle((dynamic)@event));
            DomainEventQueue.Enqueue(@event);
        }

        public void Publish<TEvent>(IEnumerable<TEvent> @events) where TEvent: IDomainEvent {
            events.ForEach(this.Publish);
        }

        public IEnumerable<IMessageContext> GetMessageContexts() {
            return SentMessageContexts;
        }
    }
}*/