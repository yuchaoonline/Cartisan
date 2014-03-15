using System;
using System.Linq.Expressions;

namespace Cartisan.Specification {
    [Semantics(Semantics.All)]
    public class AllSpecification<T> : Specification<T> {
        public override Expression<Func<T, bool>> GetExpression() {
            return o => true;
        }
    }
}