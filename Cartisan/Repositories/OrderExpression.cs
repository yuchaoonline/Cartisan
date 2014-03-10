using System;
using System.Linq.Expressions;

namespace Cartisan.Repositories {
    public class OrderExpression {
        public string OrderByField { get; set; }
        public SortOrder SortOrder { get; set; }

        public OrderExpression(string orderByField, SortOrder sortOrder = SortOrder.Unspecified) {
            this.OrderByField = orderByField;
            this.SortOrder = sortOrder;
        }
    }

    public class OrderExpression<TEntity>: OrderExpression {
        public Expression<Func<TEntity, dynamic>> OrderByExpression { get; set; }

        public OrderExpression(Expression<Func<TEntity, dynamic>> orderByExpression,
            SortOrder sortOrder = SortOrder.Unspecified): base(null, sortOrder) {
            this.OrderByExpression = orderByExpression;
        }
    }
}