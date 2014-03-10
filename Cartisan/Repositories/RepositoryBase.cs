/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cartisan.Specifications;

namespace Cartisan.Repositories {
    public abstract class RepositoryBase<TAggregateRoot>: IRepository<TAggregateRoot> where TAggregateRoot: class {

        protected abstract void DoAdd(IQueryable<TAggregateRoot> entities);
        protected abstract void DoAdd(TAggregateRoot entity);

        protected abstract TAggregateRoot DoGetByKey(params object[] keyValues);

        protected virtual IQueryable<TAggregateRoot> DoFindAll(params OrderExpression[] orderExpressions) {
            return this.DoFindAll(new AllSpecification<TAggregateRoot>(), orderExpressions);
        }

        protected abstract IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification,
            params OrderExpression[] orderExpressions);

        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification);

        protected abstract long DoCount(ISpecification<TAggregateRoot> specification);
        protected abstract long DoCount(Expression<Func<TAggregateRoot, bool>> specification);

        protected abstract bool DoExists(ISpecification<TAggregateRoot> specification);

        protected abstract void DoRemove(TAggregateRoot entity);

        protected abstract void DoUpdate(TAggregateRoot entity);

        protected abstract IQueryable<TAggregateRoot> DoPageFind(int pageIndex, int pageSize, ref long totalCount,
            ISpecification<TAggregateRoot> specification, params OrderExpression[] orderExpressions);
        protected abstract IQueryable<TAggregateRoot> DoPageFind(int pageIndex, int pageSize,
            ISpecification<TAggregateRoot> specification, params OrderExpression[] orderExpressions);

        public void Add(IQueryable<TAggregateRoot> entities) {
            this.DoAdd(entities);
        }

        public void Add(TAggregateRoot entity) {
            this.DoAdd(entity);
        }

        public TAggregateRoot GetByKey(params object[] keyValues) {
            return this.DoGetByKey(keyValues);
        }

        public long Count(ISpecification<TAggregateRoot> specification) {
            return this.DoCount(specification);
        }

        public long Count(Expression<Func<TAggregateRoot, bool>> specification) {
            return this.DoCount(specification);
        }

        public IQueryable<TAggregateRoot> FindAll(params OrderExpression[] orderByExpressions) {
            return this.DoFindAll(orderByExpressions);
        }

        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, params OrderExpression[] orderByExpressions) {
            return this.DoFindAll(specification, orderByExpressions);
        }

        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> specification, params OrderExpression[] orderByExpressions) {
            return this.DoFindAll(Specification<TAggregateRoot>.Eval(specification), orderByExpressions);
        }

        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification) {
            return this.DoFind(specification);
        }

        public TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> specification) {
            return this.DoFind(Specification<TAggregateRoot>.Eval(specification));
        }

        public bool Exists(ISpecification<TAggregateRoot> specification) {
            return this.DoExists(specification);
        }

        public bool Exists(Expression<Func<TAggregateRoot, bool>> specification) {
            return this.DoExists(Specification<TAggregateRoot>.Eval(specification));
        }

        public void Remove(TAggregateRoot entity) {
            this.DoRemove(entity);
        }

        public void Remove(IEnumerable<TAggregateRoot> entities) {
            foreach (TAggregateRoot entity in entities) {
                this.DoRemove(entity);
            }
        }

        public void Update(TAggregateRoot entity) {
            this.DoUpdate(entity);
        }

        public IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize, Expression<Func<TAggregateRoot, bool>> specification, params OrderExpression[] orderByExpressions) {
            return this.DoPageFind(pageIndex, pageSize, Specification<TAggregateRoot>.Eval(specification), orderByExpressions);
        }

        public IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize, ref long totalCount, Expression<Func<TAggregateRoot, bool>> specification,
            params OrderExpression[] orderByExpressions) {
            return this.DoPageFind(pageIndex, pageSize, ref totalCount, Specification<TAggregateRoot>.Eval(specification), orderByExpressions);
        }

        public IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize, ISpecification<TAggregateRoot> specification,
            params OrderExpression[] orderByExpressions) {
            return this.DoPageFind(pageIndex, pageSize, specification, orderByExpressions);
        }

        public IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize, ref long totalCount, ISpecification<TAggregateRoot> specification,
            params OrderExpression[] orderByExpressions) {
            return this.DoPageFind(pageIndex, pageSize, ref totalCount, specification, orderByExpressions);
        }
    }
}*/