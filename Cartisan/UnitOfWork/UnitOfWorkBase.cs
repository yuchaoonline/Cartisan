namespace Cartisan.UnitOfWork {
    public abstract class UnitOfWorkBase: IUnitOfWork {
        /*private readonly IDomainEventBus _domainEventBus;*/

        protected UnitOfWorkBase() {}

        /*protected UnitOfWorkBase(IDomainEventBus domainEventBus) {
            this._domainEventBus = domainEventBus;
        }

        protected IMessageStore MessageStore {
            get { return IoCFactory.Resolve<IMessageStore>(); }
        }

        protected IDomainEventBus DomainEventBus {
            get { return this._domainEventBus; }
        }*/

        public abstract void Commit();
    }
}