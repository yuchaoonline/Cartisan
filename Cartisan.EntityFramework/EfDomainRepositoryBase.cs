using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using Cartisan.Domain;
using Cartisan.Repositories;
using Cartisan.Specifications;

namespace Cartisan.EntityFramework {
    internal class Repository<T>: EfRepositoryBase<T> where T: class {
        public Repository(DbContext context): base(context) {}
    } 

    public abstract class EfDomainRepositoryBase<TAggregateRoot>: IDomainRepository<TAggregateRoot>, IMergeOptionChangable
        where TAggregateRoot: class, IAggregateRoot {

        protected EfDomainRepositoryBase(ContextBase context) {
            _repository = new Repository<TAggregateRoot>(context);
        }

        private IRepository<TAggregateRoot> _repository;  

        public void Add(IQueryable<TAggregateRoot> entities) {
            _repository.Add(entities);
        }

        public void Add(TAggregateRoot entity) {
            _repository.Add(entity);
        }

        public TAggregateRoot GetByKey(params object[] keyValues) {
            return _repository.GetByKey(keyValues);
        }

        public long Count(ISpecification<TAggregateRoot> specification) {
            return _repository.Count(specification);
        }

        public long Count(Expression<Func<TAggregateRoot, bool>> specification) {
            return _repository.Count(specification);
        }

        public IQueryable<TAggregateRoot> FindAll(params OrderExpression[] orderByExpressions) {
            return _repository.FindAll(orderByExpressions);
        }

        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification,
            params OrderExpression[] orderByExpressions) {
            return _repository.FindAll(orderByExpressions);
        }

        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> specification,
            params OrderExpression[] orderByExpressions) {
            return _repository.FindAll(specification);
        }

        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification) {
            return _repository.Find(specification);
        }

        public TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> specification) {
            return _repository.Find(specification);
        }

        public bool Exists(ISpecification<TAggregateRoot> specification) {
            return _repository.Exists(specification);
        }

        public bool Exists(Expression<Func<TAggregateRoot, bool>> specification) {
            return _repository.Exists(specification);
        }

        public void Remove(TAggregateRoot entity) {
            _repository.Remove(entity);
        }

        public void Remove(IEnumerable<TAggregateRoot> entities) {
            _repository.Remove(entities);
        }

        public void Update(TAggregateRoot entity) {
            _repository.Update(entity);
        }

        public IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize,
            Expression<Func<TAggregateRoot, bool>> specification, params OrderExpression[] orderByExpressions) {
            return _repository.PageFind(pageIndex, pageSize, specification, orderByExpressions);
        }

        public IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize, ref long totalCount,
            Expression<Func<TAggregateRoot, bool>> specification,
            params OrderExpression[] orderByExpressions) {
            return _repository.PageFind(pageIndex, pageSize, ref totalCount, specification, orderByExpressions);
        }

        public IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize,
            ISpecification<TAggregateRoot> specification,
            params OrderExpression[] orderByExpressions) {
                return _repository.PageFind(pageIndex, pageSize, specification, orderByExpressions);
        }

        public IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize, ref long totalCount,
            ISpecification<TAggregateRoot> specification,
            params OrderExpression[] orderByExpressions) {
                return _repository.PageFind(pageIndex, pageSize, ref totalCount, specification, orderByExpressions);
        }

        public void ChangeMergeOption<TEntity>(MergeOption mergeOption) where TEntity: class, IAggregateRoot {
            IMergeOptionChangable mergeOptionChangable = _repository as IMergeOptionChangable;
            if(mergeOptionChangable!=null) {
                mergeOptionChangable.ChangeMergeOption<TEntity>(mergeOption);
            }
        }
    }
}