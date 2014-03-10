using System;
using System.Linq.Expressions;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Specifications {
    [Semantics(Semantics.And)]
    public class AndSpecification<T>:CompositeSpecification<T> {
        public AndSpecification(ISpecification<T> left, ISpecification<T> right): base(left, right) {}
        public override Expression<Func<T, bool>> GetExpression() {
            Expression<Func<T, bool>> body = Left.GetExpression().And(Right.GetExpression());
            return Expression.Lambda<Func<T, bool>>(body, Left.GetExpression().Parameters);
        }
    }
}