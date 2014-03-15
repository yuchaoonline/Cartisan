using System;
using System.Linq.Expressions;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Specification {
    [Semantics(Semantics.And)]
    public class AndSpecification<T>:CompositeSpecification<T> {
        public AndSpecification(ISpecification<T> left, ISpecification<T> right): base(left, right) {}
        public override Expression<Func<T, bool>> GetExpression() {
            Expression<Func<T, bool>> body = this.Left.GetExpression().And(this.Right.GetExpression());
            return Expression.Lambda<Func<T, bool>>(body, this.Left.GetExpression().Parameters);
        }
    }
}