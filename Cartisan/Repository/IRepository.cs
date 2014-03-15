using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cartisan.Domain;
using Cartisan.Specification;

namespace Cartisan.Repository {
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public interface IRepository<TAggregateRoot> 
        where TAggregateRoot: class, IAggregateRoot {

        void Add(IQueryable<TAggregateRoot> entities);
        void Add(TAggregateRoot entity);

        TAggregateRoot Get(params object[] keyValues);
        TAggregateRoot Load(params object[] keyValues);

        long Count(ISpecification<TAggregateRoot> specification);
        long Count(Expression<Func<TAggregateRoot, bool>> specification);

        IQueryable<TAggregateRoot> FindAll(params OrderExpression[] orderByExpressions);

        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification,
            params OrderExpression[] orderByExpressions);

        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> specification,
            params OrderExpression[] orderByExpressions);

        TAggregateRoot Find(ISpecification<TAggregateRoot> specification);
        TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> specification);

        bool Exists(ISpecification<TAggregateRoot> specification);
        bool Exists(Expression<Func<TAggregateRoot, bool>> specification);

        void Remove(TAggregateRoot entity);
        void Remove(IEnumerable<TAggregateRoot> entities);

        void Update(TAggregateRoot entity);

        IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize,
            Expression<Func<TAggregateRoot, bool>> specification, params OrderExpression[] orderByExpressions);
        IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize, ref long totalCount,
            Expression<Func<TAggregateRoot, bool>> specification, params OrderExpression[] orderByExpressions);
        IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize,
            ISpecification<TAggregateRoot> specification, params OrderExpression[] orderByExpressions);
        IQueryable<TAggregateRoot> PageFind(int pageIndex, int pageSize, ref long totalCount,
            ISpecification<TAggregateRoot> specification, params OrderExpression[] orderByExpressions);
    }
}