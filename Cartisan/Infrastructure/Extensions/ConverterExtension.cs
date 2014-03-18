using System;
using System.ComponentModel;
using System.Globalization;

namespace Cartisan.Infrastructure.Extensions {
    public static class ConverterExtension {
        public static TypeConverter GetCustomTypeConverter(Type type) {
            return TypeDescriptor.GetConverter(type);
        }

        public static T To<T>(this object value) {
            return (T)To(value, typeof(T));
        }

        public static object To(this object value, Type destinationType) {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        public static object To(object value, Type destinationType, CultureInfo culture) {
            if (value != null) {
                Type sourceType = value.GetType();

                TypeConverter destinationConverter = GetCustomTypeConverter(destinationType);
                TypeConverter sourceConvert = GetCustomTypeConverter(sourceType);

                if (destinationConverter != null && destinationConverter.CanConvertFrom(sourceType)) {
                    return destinationConverter.ConvertFrom(null, culture, value);
                }
                if (sourceConvert != null && sourceConvert.CanConvertTo(destinationType)) {
                    return sourceConvert.ConvertTo(null, culture, value, destinationType);
                }
                if (destinationType.IsEnum && value is int) {
                    return Enum.ToObject(destinationType, (int)value);
                }
                if (!destinationType.IsAssignableFrom(sourceType)) {
                    return Convert.ChangeType(value, destinationType, culture);
                }
            }

            return value;
        } 
    }
}