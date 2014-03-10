using System;
using Cartisan.Event;
using Cartisan.Infrastructure;

namespace Cartisan.Domain {
    public abstract class AggregateRoot: Entity, IAggregateRoot {
        protected IDomainEventBus EventBus {
            get { return IoCFactory.Resolve<IDomainEventBus>(); }
        }

        private string _aggreagetRootType;

        protected string AggregateRootName {
            get {
                if(string.IsNullOrWhiteSpace(_aggreagetRootType)) {
                    Type aggregateRootType = this.GetType();
                    if("EntityProxyModule"==this.GetType().Module.ToString()) {
                        aggregateRootType = aggregateRootType.BaseType;
                    }
                    _aggreagetRootType = aggregateRootType.FullName;
                }
                return _aggreagetRootType;
            }
        }

        protected virtual void OnEvent<TDomainEvent>(TDomainEvent @event) where TDomainEvent: class, IDomainEvent {
            HandleEvent(@event);
            @event.AggregateRootName = AggregateRootName;
            EventBus.Publish(@event);
        }

        private void HandleEvent<TDomainEvent>(TDomainEvent @event) where TDomainEvent: class, IDomainEvent {
            IEventSubscriber<TDomainEvent> subscriber = this as IEventSubscriber<TDomainEvent>;
            if(subscriber!=null) {
                subscriber.Handle(@event);
            }
        }
    }
}