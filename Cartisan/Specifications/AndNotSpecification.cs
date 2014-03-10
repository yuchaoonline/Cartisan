using System;
using System.Linq.Expressions;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Specifications {
    [Semantics(Semantics.AndNot)]
    public class AndNotSpecification<T>: CompositeSpecification<T> {
        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right): base(left, right) {}
        public override Expression<Func<T, bool>> GetExpression() {
            Expression<Func<T, bool>> bodyNot = Expression.Lambda<Func<T, bool>>(Expression.Not(Right.GetExpression().Body));
            Expression<Func<T, bool>> body = Left.GetExpression().And(bodyNot);
            return Expression.Lambda<Func<T, bool>>(body, Left.GetExpression().Parameters);
        }
    }
}