using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cartisan.Infrastructure.Extensions {
    public static class CollectionExtension {
        private static bool TryDo(Action action) {
            try {
                action();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public static bool TryRemove(this Hashtable hashtable, object key) {
            return TryDo(() => hashtable.Remove(key));
        }

        public static bool TryRemove(this IDictionary collection, object key) {
            return TryDo(() => collection.Remove(key));
        }

        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source) {
            return source ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach(T element in source.OrEmptyIfNull())
                action(element);
            return source;
        }

        public static IQueryable<T> GetPageElements<T>(this IQueryable<T> queryable, int pageIndex, int pageSize) {
            return queryable.Skip(pageIndex * pageSize).Take(pageSize);
        } 
    }
}