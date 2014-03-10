namespace Cartisan.Domain {
    public abstract class Entity: IEntity {
        public object DomainContext { get; set; }
    }
}