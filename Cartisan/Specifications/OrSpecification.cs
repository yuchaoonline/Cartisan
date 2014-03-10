using System;
using System.Linq.Expressions;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Specifications {
    public class OrSpecification<T>: CompositeSpecification<T> {
        public OrSpecification(ISpecification<T> left, ISpecification<T> right): base(left, right) {}
        public override Expression<Func<T, bool>> GetExpression() {
            Expression<Func<T, bool>> body = Left.GetExpression().Or(Right.GetExpression());
            return Expression.Lambda<Func<T, bool>>(body, Left.GetExpression().Parameters);
        }
    }
}