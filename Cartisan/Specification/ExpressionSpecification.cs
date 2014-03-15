﻿using System;
using System.Linq.Expressions;

namespace Cartisan.Specification {
    internal sealed class ExpressionSpecification<T>: Specification<T> {
        private readonly Expression<Func<T, bool>> _expression;

        public ExpressionSpecification(Expression<Func<T, bool>> expression) {
            this._expression = expression;
        }

        public override Expression<Func<T, bool>> GetExpression() {
            return this._expression;
        }
    }
}