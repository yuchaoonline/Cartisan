using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Cartisan.Domain;
using Cartisan.Infrastructure.Extensions;
using Cartisan.Repository;
using Cartisan.Specification;

namespace Cartisan.EntityFramework {
    public abstract class EfRepositoryBase<TEntity>: IRepository<TEntity>, IMergeOptionChangable
        where TEntity: class, IAggregateRoot {
        private DbContext _context;

        public EfRepositoryBase(DbContext context) {
            if(context == null) {
                throw new ArgumentException("repository could not work without dbContext");
            }
            this._context = context;
        }

        private DbSet<TEntity> _dbSet;

        private DbSet<TEntity> DbSet {
            get { return _dbSet ?? (_dbSet = _context.Set<TEntity>()); }
        }

        public void Add(IQueryable<TEntity> entities) {
            entities.ForEach(entity => DbSet.Add(entity));
        }

        public void Add(TEntity entity) {
            DbSet.Add(entity);
        }

        public TEntity Get(params object[] keyValues) {
            return DbSet.Find(keyValues);
        }

        public TEntity Load(params object[] keyValues) {
            return DbSet.Find(keyValues);
        }

        public TEntity Find(ISpecification<TEntity> specification) {
            return this.Find(specification.GetExpression());
        }

        public TEntity Find(Expression<Func<TEntity, bool>> specification) {
            return DbSet.Where(specification).FirstOrDefault();
        }

        public IQueryable<TEntity> FindAll(params OrderExpression[] orderByExpressions) {
            return this.FindAll(new AllSpecification<TEntity>(), orderByExpressions);
        }

        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification,
            params OrderExpression[] orderByExpressions) {
            return this.FindAll(specification.GetExpression(), orderByExpressions);
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> specification,
            params OrderExpression[] orderByExpressions) {
            IQueryable<TEntity> query = DbSet.Where(specification);
            bool hasSorted = false;
            orderByExpressions.ForEach(orderExpression => {
                query = query.MergeOrderExpression(orderExpression, hasSorted);
                hasSorted = true;
            });
            return query;
        }

        public long Count(ISpecification<TEntity> specification) {
            return DbSet.LongCount(specification.GetExpression());
        }

        public long Count(Expression<Func<TEntity, bool>> specification) {
            return DbSet.LongCount(specification);
        }

        public bool Exists(ISpecification<TEntity> specification) {
            return Count(specification) > 0;
        }

        public bool Exists(Expression<Func<TEntity, bool>> specification) {
            return Count(specification) > 0;
        }

        public void Remove(TEntity entity) {
            DbSet.Remove(entity);
        }

        public void Remove(IEnumerable<TEntity> entities) {
            entities.ForEach(entity => DbSet.Remove(entity));
        }

        public void Update(TEntity entity) {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> PageFind(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> specification,
            params OrderExpression[] orderByExpressions) {
            if(pageIndex < 0) {
                throw new ArgumentException("PageIndex无效");
            }
            if(pageSize < 0) {
                throw new ArgumentException("PageCount无效");
            }
            if(orderByExpressions == null || orderByExpressions.Length == 0) {
                throw new ArgumentNullException("OrderByExpressions不能为空");
            }
            if(specification == null) {
                specification = new AllSpecification<TEntity>().GetExpression();
            }
            var query = this.FindAll(specification, orderByExpressions);
            return query.GetPageElements(pageIndex, pageSize);
        }

        public IQueryable<TEntity> PageFind(int pageIndex, int pageSize, ref long totalCount,
            Expression<Func<TEntity, bool>> specification, params OrderExpression[] orderByExpressions) {
            var query = this.PageFind(pageIndex, pageSize, specification, orderByExpressions);
            totalCount = Count(specification);

            return query;
        }

        public IQueryable<TEntity> PageFind(int pageIndex, int pageSize, ISpecification<TEntity> specification,
            params OrderExpression[] orderByExpressions) {
            return PageFind(pageIndex, pageSize, specification.GetExpression(), orderByExpressions);
        }

        public IQueryable<TEntity> PageFind(int pageIndex, int pageSize, ref long totalCount,
            ISpecification<TEntity> specification,
            params OrderExpression[] orderByExpressions) {
            return PageFind(pageIndex, pageSize, ref totalCount, specification.GetExpression(), orderByExpressions);
        }

        public void ChangeMergeOption<TMergeOptionEntity>(MergeOption mergeOption)
            where TMergeOptionEntity: class, IAggregateRoot {
            ObjectContext objectContext = ((IObjectContextAdapter)_context).ObjectContext;
            ObjectSet<TMergeOptionEntity> set = objectContext.CreateObjectSet<TMergeOptionEntity>();
            set.MergeOption = mergeOption;
        }
    }
}