using System;
using System.Linq;

namespace Cartisan.Infrastructure.Extensions {
    public static class PaginatedExtension {
        //Todo： 分页需要主动排序吗
        /*public static Paginated<T> Paginate<TKey, T>(this IQueryable<T> query, int pageIndex,
            int pageSize, Expression<Func<T, TKey>> orderBySelector, bool isDescending = false) {
            query = isDescending ? query.OrderByDescending(orderBySelector) : query.OrderBy(orderBySelector);
            return Paginate(query, pageIndex, pageSize);
        }

        public static Paginated<T> Paginate<TKey, T>(this IQueryable<T> query, int pageIndex,
            int pageSize, Expression<Func<T, TKey>> orderBySelector, IComparer<TKey> comparer, bool isDescending = false) {
            query = isDescending ? query.OrderByDescending(orderBySelector, comparer) : query.OrderBy(orderBySelector);
            return Paginate(query, pageIndex, pageSize);
        }*/

        /// <summary>
        /// 对查询进行分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Paginated<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize) {
            if (pageIndex <= 0) {
                throw new ArgumentException("pageIndex必须大于等于零。", "pageIndex");
            }
            if (pageSize <= 0) {
                throw new ArgumentException("pageSize必须大于等于零。", "pageSize");
            }
            int totalCount = query.Count();

            IQueryable<T> collection = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new Paginated<T>(collection, pageIndex, pageSize, totalCount);
        }
    }
}