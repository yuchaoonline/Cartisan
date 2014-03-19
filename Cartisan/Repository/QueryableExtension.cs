using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cartisan.Repository {
    public static class QueryableExtension {
        public static PaginatedList<T> ToPaginatedList<TKey, T>(this IQueryable<T> query, int pageIndex,
            int pageSize, Expression<Func<T, TKey>> orderBySelector, bool isDescending = false) {
            query = isDescending ? query.OrderByDescending(orderBySelector) : query.OrderBy(orderBySelector);
            return Paginate(query, pageIndex, pageSize);
        }

        public static PaginatedList<T> ToPaginatedList<TKey, T>(this IQueryable<T> query, int pageIndex,
            int pageSize, Expression<Func<T, TKey>> orderBySelector, IComparer<TKey> comparer, bool isDescending = false) {
            query = isDescending ? query.OrderByDescending(orderBySelector, comparer) : query.OrderBy(orderBySelector);
            return Paginate(query, pageIndex, pageSize);
        }

        private static PaginatedList<T> Paginate<T>(IQueryable<T> query, int pageIndex, int pageSize) {
            if (pageIndex <= 0) {
                throw new ArgumentException("pageIndex必须大于等于零。", "pageIndex");
            }
            if (pageSize <= 0) {
                throw new ArgumentException("pageSize必须大于等于零。", "pageSize");
            }
            int totalCount = query.Count();

            IQueryable<T> collection = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PaginatedList<T>(collection, pageIndex, pageSize, totalCount);
        } 
    }
}