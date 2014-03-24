using System.Linq;
using System.Linq.Expressions;
using Cartisan.Infrastructure.Extensions;
using Cartisan.Infrastructure.Utility;
using Cartisan.Repository;

namespace Cartisan.EntityFramework.Extensions {
    public static class OrderExpressionExtension {

        public static IQueryable<TEntity> MergeOrderExpression<TEntity>(this IQueryable<TEntity> query,
            OrderExpression orderExpression, bool hasSorted = false) {
            string orderByCmd;

            if(hasSorted) {
                if(orderExpression.SortOrder==SortOrder.Descending) {
                    orderByCmd = "ThenByDescending";
                }
                else {
                    orderByCmd = "ThenBy";
                }
            }
            else {
                if (orderExpression.SortOrder == SortOrder.Descending) {
                    orderByCmd = "OrderByDescending";
                }
                else {
                    orderByCmd = "OrderBy";
                }
            }
            LambdaExpression le = null;

            if(orderExpression is OrderExpression<TEntity>) {
                le = LambdaUitls.GetLambdaExpression(typeof(TEntity),
                    ((orderExpression as OrderExpression<TEntity>).OrderByExpression.Body
                        .GetValueByKey<MemberExpression>("Operand").Member.Name));
            }
            else if(!string.IsNullOrWhiteSpace(orderExpression.OrderByField)) {
                le = LambdaUitls.GetLambdaExpression(typeof(TEntity), orderExpression.OrderByField);
            }

            MethodCallExpression orderByCallExpression = Expression.Call(typeof(Queryable), orderByCmd,
                new[] {typeof(TEntity), le.Body.Type}, query.Expression, le);

            return query.Provider.CreateQuery<TEntity>(orderByCallExpression);
        } 
    }
}