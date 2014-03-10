namespace Cartisan.Event {
    public interface IDomainEvent: IEvent {
        object AggregateRootId { get; }
        string AggregateRootName { get; set; }
    }
}