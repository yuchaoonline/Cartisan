using System;
using System.Linq.Expressions;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Specification {
    public class OrSpecification<T>: CompositeSpecification<T> {
        public OrSpecification(ISpecification<T> left, ISpecification<T> right): base(left, right) {}
        public override Expression<Func<T, bool>> GetExpression() {
            Expression<Func<T, bool>> body = this.Left.GetExpression().Or(this.Right.GetExpression());
            return Expression.Lambda<Func<T, bool>>(body, this.Left.GetExpression().Parameters);
        }
    }
}