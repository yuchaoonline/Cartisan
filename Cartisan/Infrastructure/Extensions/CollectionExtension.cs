using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cartisan.Infrastructure.Utility;

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

        /// <summary>
        /// 尝试移除指定项
        /// </summary>
        /// <param name="hashtable"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool TryRemove(this Hashtable hashtable, object key) {
            return TryDo(() => hashtable.Remove(key));
        }

        /// <summary>
        /// 尝试移除指定项
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool TryRemove(this IDictionary collection, object key) {
            return TryDo(() => collection.Remove(key));
        }

        /// <summary>
        /// 如果为Null，返回一个空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source) {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// 为每一个执行指定的操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach(T element in source.OrEmptyIfNull())
                action(element);
            return source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="initial"></param>
        /// <param name="collection"></param>
        public static void AddRange<T>(this IList<T> initial, IEnumerable<T> collection) {
            ValidationUtils.ArgumentNotNull(initial, "initial");

            if(collection==null) {
                return;
            }

            foreach(T value in collection) {
                initial.Add(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate) {
            int index = 0;
            foreach(T value in collection) {
                if(predicate(value)) {
                    return index;
                }
                index++;
            }

            return -1;
        }
    }
}