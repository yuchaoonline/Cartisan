using System.Collections.Generic;

namespace Cartisan.Infrastructure.Extensions {
    public static class StringExtensions {
        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="quantity">重复次数</param>
        /// <returns></returns>
        public static string Repeat(this string source, int quantity) {
            return source.Repeat("", quantity);
        }

        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="quantity">重复次数</param>
        /// <returns></returns>
        public static string Repeat(this string source, string separator, int quantity) {
            var strs = new List<string>(quantity);
            for(int i = 0; i < quantity; i++) {
                strs.Add(source);
            }
            return string.Join(separator, strs);
        }
    }
}