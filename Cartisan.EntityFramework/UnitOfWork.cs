using System.Collections.Generic;
using System.Data.Entity;
using System.Transactions;
using Cartisan.Config;
using Cartisan.UnitOfWork;

namespace Cartisan.EntityFramework {
    public class UnitOfWork: UnitOfWorkBase, IUnitOfWork {
        private readonly List<DbContext> _contexts;

        public UnitOfWork() {
            _contexts = new List<DbContext>();
        }

        /*public UnitOfWork(IDomainEventBus domainEventBus): base(domainEventBus) {
            _contexts = new List<DbContext>();
        }*/

        public override void Commit() {
            using(TransactionScope scope = new TransactionScope()) {
                _contexts.ForEach(context => context.SaveChanges());
                scope.Complete();
            }

            /*if(DomainEventBus!=null) {
                DomainEventBus.Commit();
            }*/

            if(Configuration.IsPersistanceMessage) {
                // TODO: persistance command and domain events
                /*var currentCommandContext = PerMessageContextLifetimeManager.CurrentMessageContext;
                var domainEventContexts = _DomainEventBus.GetMessageContexts();
                MessageStore.Save(currentCommandContext, domainEventContexts);*/
            }
        }

        internal void RegisterDbContext(DbContext context) {
            if(!_contexts.Exists(dbContext => Equals(dbContext, context))) {
                _contexts.Add(context);
            }
        }
    }
}