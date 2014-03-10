using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cartisan.Infrastructure.Utility {
    public class LambdaUitl {
        public static LambdaExpression GetLambdaExpression(Type type, string propertyName) {
            ParameterExpression param = Expression.Parameter(type);
            PropertyInfo property = type.GetProperty(propertyName);
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            var le = Expression.Lambda(propertyAccessExpression, param);
            return le;
        }

        public static IQueryable<TEntity> GetOrderByQueryable<TEntity>(IQueryable<TEntity> query, LambdaExpression orderByExpression, bool asc)
            where TEntity: class {
            var orderBy = asc ? "OrderBy" : "OrderByDescending";
            MethodCallExpression orderByCallExpression =
                        Expression.Call(typeof(Queryable),
                        orderBy,
                        new Type[] { typeof(TEntity),
                        orderByExpression.Body.Type},
                        query.Expression,
                        orderByExpression);
            return query.Provider.CreateQuery<TEntity>(orderByCallExpression);
        } 
    }
}