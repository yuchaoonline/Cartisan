using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using Cartisan.EntityFramework.Extensions;
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
            UnitOfWork unitOfWork = ServiceLocator.GetService<IUnitOfWork>() as UnitOfWork;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            this.RegisterMaps(modelBuilder, this.GetType().Namespace + ".Mappings");

            //            IEnumerable<Type> typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //                .Where(type => !string.IsNullOrEmpty(type.Namespace))
            //                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
            //                    type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            //
            //            foreach(var type in typesToRegister) {
            //                dynamic configurationInstance = Activator.CreateInstance(type);
            //                modelBuilder.Configurations.Add(configurationInstance);
            //            }
            //            base.OnModelCreating(modelBuilder);
        }

        protected virtual void RegisterMaps(DbModelBuilder modelBuilder, params string[] namespaces) {
            IEnumerable<Type> mapTypes = Assembly.GetAssembly(this.GetType()).GetTypes()
                .Where(type => namespaces.Contains(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                    (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                    || type.BaseType.GetGenericTypeDefinition()==typeof(ComplexTypeConfiguration<>)));

            foreach (Type mapType in mapTypes) {
                dynamic mapInstance = Activator.CreateInstance(mapType);
                modelBuilder.Configurations.Add(mapInstance);
            }
        }



        public void SetAsAdded<TEntity>(TEntity entity) where TEntity: class {
            DbEntityEntry dbEntityEntry = this.GetDbEntityEntry(entity);
            dbEntityEntry.State = EntityState.Added;
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity: class {
            DbEntityEntry dbEntityEntry = this.GetDbEntityEntry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity: class {
            DbEntityEntry dbEntityEntry = this.GetDbEntityEntry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        private DbEntityEntry GetDbEntityEntry<TEntity>(TEntity entity) where TEntity: class {
            DbEntityEntry<TEntity> dbEntityEntry = Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached) {
                this.Set<TEntity>().Attach(entity);
            }

            return dbEntityEntry;
        }

        public void Commit() {
            this.SaveChanges();
        }
    }
}