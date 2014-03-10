namespace Cartisan.Event.Default {
    public class DomainEvent: IDomainEvent {
        public object AggregateRootId { get; private set; }
        public string AggregateRootName { get; set; }

        public DomainEvent(object aggregateRootId) {
            this.AggregateRootId = aggregateRootId;
        }
    }
}