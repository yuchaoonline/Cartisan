using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Cartisan.Domain;
using Cartisan.Infrastructure.Extensions;
using Cartisan.Repositories;
using Cartisan.Specifications;

namespace Cartisan.EntityFramework {
    public abstract class EfRepositoryBase<TEntity>: RepositoryBase<TEntity>, IMergeOptionChangable where TEntity: class {
        private DbContext _context;
        public EfRepositoryBase(DbContext context) {
            if(context==null) {
                throw new ArgumentException("repository could not work without dbContext");
            }
            this._context = context;
        }

        private DbSet<TEntity> _dbSet; 
        private DbSet<TEntity> DbSet {
            get { return _dbSet ?? (_dbSet = _context.Set<TEntity>()); }
        } 

        protected override void DoAdd(IQueryable<TEntity> entities) {
            entities.ForEach(entity => DbSet.Add(entity));
        }

        protected override void DoAdd(TEntity entity) {
            DbSet.Add(entity);
        }

        protected override TEntity DoGetByKey(params object[] keyValues) {
            return DbSet.Find(keyValues);
        }

        protected override IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification, params OrderExpression[] orderExpressions) {
            IQueryable<TEntity> query = DbSet.Where(specification.GetExpression());
            bool hasSorted = false;
            orderExpressions.ForEach(orderExpression => {
                query = query.MergeOrderExpression(orderExpression, hasSorted);
                hasSorted = true;
            });
            return query;
        }

        protected override TEntity DoFind(ISpecification<TEntity> specification) {
            return DbSet.Where(specification.GetExpression()).FirstOrDefault();
        }

        protected override long DoCount(ISpecification<TEntity> specification) {
            return DbSet.LongCount(specification.GetExpression());
        }

        protected override long DoCount(Expression<Func<TEntity, bool>> specification) {
            return DbSet.LongCount(specification);
        }

        protected override bool DoExists(ISpecification<TEntity> specification) {
            return Count(specification) > 0;
        }

        protected override void DoRemove(TEntity entity) {
            DbSet.Remove(entity);
        }

        protected override void DoUpdate(TEntity entity) {
            throw new NotImplementedException();
        }

        protected override IQueryable<TEntity> DoPageFind(int pageIndex, int pageSize, ref long totalCount, ISpecification<TEntity> specification,
            params OrderExpression[] orderExpressions) {
            var query = DoPageFind(pageIndex, pageSize, specification, orderExpressions);
            totalCount = Count(specification.GetExpression());

            return query;
        }

        protected override IQueryable<TEntity> DoPageFind(int pageIndex, int pageSize, ISpecification<TEntity> specification,
            params OrderExpression[] orderExpressions) {
            if(pageIndex<0) {
                throw new ArgumentException("InvalidPageIndex");
            }
            if(pageSize<0) {
                throw new ArgumentException("InvalidPageCount");
            }
            if(orderExpressions==null || orderExpressions.Length==0) {
                throw new ArgumentNullException("OrderByExpressionCannotBeNull");
            }
            if(specification==null) {
                specification = new AllSpecification<TEntity>();
            }
            var query = this.DoFindAll(specification, orderExpressions);
            return query.GetPageElements(pageIndex, pageSize);
        }

        public void ChangeMergeOption<TMergeOptionEntity>(MergeOption mergeOption) where TMergeOptionEntity: class, IAggregateRoot {
            ObjectContext objectContext = ((IObjectContextAdapter)_context).ObjectContext;
            ObjectSet<TMergeOptionEntity> set = objectContext.CreateObjectSet<TMergeOptionEntity>();
            set.MergeOption = mergeOption;
        }
    }
}