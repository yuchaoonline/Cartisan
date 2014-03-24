using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cartisan.Infrastructure.Utility;

namespace Cartisan.Infrastructure.Extensions {
    public static class ExpressionExtensions {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge) {
            Dictionary<ParameterExpression, ParameterExpression> map =
                first.Parameters.Select((f, i) => new {f, s = second.Parameters[i]}).ToDictionary(p => p.s, p => p.f);
            Expression secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second) {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second) {
            return first.Compose(second, Expression.Or);
        }
    }
}