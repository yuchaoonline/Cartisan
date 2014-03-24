using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cartisan.Infrastructure.Utility {
    public class LambdaUitls {
        public static LambdaExpression GetLambdaExpression(Type type, string propertyName) {
            ParameterExpression param = Expression.Parameter(type);
            PropertyInfo property = type.GetProperty(propertyName);
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            var le = Expression.Lambda(propertyAccessExpression, param);
            return le;
        }

        public static Expression<Func<T, bool>> True<T>() {
            return f => true;
        }

        public static Expression<Func<T, bool>> False<T>() {
            return f => false;
        } 
    }

    public class ParameterRebinder: ExpressionVisitor {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map) {
            this._map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map,
            Expression exp) {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p) {
            ParameterExpression replacement;
            if (_map.TryGetValue(p, out replacement)) {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}