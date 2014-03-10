using System;
using System.Linq.Expressions;

namespace Cartisan.Specifications {
    [Semantics(Semantics.All)]
    public class AllSpecification<T> : Specification<T> {
        public override Expression<Func<T, bool>> GetExpression() {
            return o => true;
        }
    }
}