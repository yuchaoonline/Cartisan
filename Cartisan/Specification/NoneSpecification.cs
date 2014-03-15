using System;
using System.Linq.Expressions;

namespace Cartisan.Specification {
    [Semantics(Semantics.None)]
    public class NoneSpecification<T>: Specification<T> {
        public override Expression<Func<T, bool>> GetExpression() {
            return o => false;
        }
    }
}