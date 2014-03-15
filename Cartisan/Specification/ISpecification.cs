using System;
using System.Linq.Expressions;

namespace Cartisan.Specification {
    public interface ISpecification<T> {
        bool IsSatisfiedBy(T obj);
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        ISpecification<T> AndNot(ISpecification<T> other);
        ISpecification<T> Not();
        Expression<Func<T, bool>> GetExpression();
    }
}