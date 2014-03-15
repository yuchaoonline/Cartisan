using System;
using System.Linq.Expressions;

namespace Cartisan.Specification {
    [Semantics(Semantics.Not)]
    public class NotSpecification<T>:Specification<T> {
        private readonly ISpecification<T> _specification;

        public NotSpecification(ISpecification<T> specification) {
            this._specification = specification;
        }

        public override Expression<Func<T, bool>> GetExpression() {
            UnaryExpression body = Expression.Not(this._specification.GetExpression().Body);
            return Expression.Lambda<Func<T, bool>>(body, this._specification.GetExpression().Parameters);
        }
    }
}