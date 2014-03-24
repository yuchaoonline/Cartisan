using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using Cartisan.EntityFramework.Extensions;
using Cartisan.Infrastructure;
using Cartisan.IoC;
using Cartisan.UnitOfWork;

namespace Cartisan.EntityFramework {
    public class ContextBase: DbContext {
        protected ContextBase() {
            this.RegisterToUnitOfWork();
            this.InitObjectContext();
        }

        protected ContextBase(DbCompiledModel model)
            : base(model) {
            this.InitObjectContext();
            this.RegisterToUnitOfWork();
        }

        protected ContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString) {
            this.InitObjectContext();
            this.RegisterToUnitOfWork();
        }

        protected ContextBase(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection) {
            this.InitObjectContext();
            this.InitObjectContext();
            this.RegisterToUnitOfWork();
        }

        protected ContextBase(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model) {
            this.InitObjectContext();
            this.RegisterToUnitOfWork();
        }

        protected ContextBase(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection) {
            this.InitObjectContext();
            this.RegisterToUnitOfWork();
        }

        protected ContextBase(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext) {
            this.InitObjectContext();
            this.RegisterToUnitOfWork();
        }

        private void RegisterToUnitOfWork() {
            UnitOfWork unitOfWork = IoCFactory.Resolve<IUnitOfWork>() as UnitOfWork;
            if (unitOfWork != null) {
                unitOfWork.RegisterDbContext(this);
            }
        }

        private void InitObjectContext() {
            ObjectContext objectContext = (this as IObjectContextAdapter).ObjectContext;
            if (objectContext != null) {
                objectContext.ObjectMaterialized +=
                    (s, e) => this.InitializeQueryableCollections(e.Entity);
            }
        }
    }
}