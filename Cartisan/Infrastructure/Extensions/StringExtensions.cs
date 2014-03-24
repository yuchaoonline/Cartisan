using System.Collections.Generic;
using System.Globalization;
using System.Text;

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
            for (int i = 0; i < quantity; i++) {
                strs.Add(source);
            }
            return string.Join(separator, strs);
        }

        /// <summary>
        /// 转换成驼峰式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string str) {
            if (string.IsNullOrEmpty(str)) {
                return str;
            }
            if (!char.IsUpper(str[0])) {
                return str;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++) {
                bool hasNext = (i + 1) < str.Length;
                if ((i == 0 || !hasNext) || char.IsUpper(str[i + 1])) {
                    char lowerCase = char.ToLower(str[i], CultureInfo.InvariantCulture);
                    sb.Append(lowerCase);
                }
                else {
                    sb.Append(str.Substring(i));
                    break;
                }
            }
            return sb.ToString();
        }
    }
}