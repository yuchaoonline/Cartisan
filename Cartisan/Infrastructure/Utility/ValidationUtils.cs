using System;
using System.Globalization;

namespace Cartisan.Infrastructure.Utility {
    public static class ValidationUtils {
        public static void ArgumentNotNullOrEmpty(string value, string parameterName) {
            ArgumentNotNull(value, parameterName);

            if(value.Length==0) {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "'{0}'不能为空。", parameterName),
                    parameterName);
            }
        }

        public static void ArgumentTypeIsEnum(Type enumType, string parameterName) {
            ArgumentNotNull(enumType, parameterName);

            if(!enumType.IsEnum) {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "类型{0}不是枚举。", parameterName),
                    parameterName);
            }
        }

        public static void ArgumentNotNull(object value, string parameterName) {
            if (value == null) {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}