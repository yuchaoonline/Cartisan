using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Cartisan.Infrastructure.Utility;

namespace Cartisan.Infrastructure.Extensions {
    public static class ObjectExtension {
        public static object InvokeGenericMethod(this object obj, Type genericType, string method, object[] args) {
            MethodInfo mi =
                obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).First(
                    m => m.Name == method && m.IsGenericMethod);
            MethodInfo miConstructed = mi.MakeGenericMethod(genericType);
            FastInvoke.FastInvokeHandler fastInvoker = FastInvoke.GetMethodInvoker(miConstructed);
            return fastInvoker(obj, args);
        }

        public static object InvokeMethod(this object obj, string method, object[] args) {
            MethodInfo mi = null;
            foreach(var m in 
                obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) {
                if(m.Name == method && m.GetParameters().Length == args.Length) {
                    bool equalParameters = true;
                    for(int i = 0; i < m.GetParameters().Length; i++) {
                        var type = m.GetParameters()[i];
                        if(!type.ParameterType.IsInstanceOfType(args[i])) {
                            equalParameters = false;
                            break;
                        }
                    }
                    if(equalParameters) {
                        mi = m;
                        break;
                    }
                }
            }
            if(mi == null) {
                throw new NotSupportedException();
            }
            FastInvoke.FastInvokeHandler fastInvoker = FastInvoke.GetMethodInvoker(mi);
            return fastInvoker(obj, args);
        }

        public static string GetPropertyName<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression) {
            var property = GetProperty(model, expression);

            if (property != null) {
                return property.Name;
            }

            return null;
        }

        private static PropertyInfo GetProperty<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression) {
            return GetMember(model, expression) as PropertyInfo;
        }

        private static MemberInfo GetMember<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression) {
            return ((MemberExpression)expression.Body).Member;
        }

        public static bool HasAttribute<TAttribute>(this MemberInfo member) where TAttribute: Attribute {
            return member.GetAttribute<TAttribute>() != null;
        }

        public static TAttribute GetAttribute<TAttribute>(this MemberInfo member) where TAttribute: Attribute {
            object[] attributes = member.GetCustomAttributes(typeof(TAttribute), true);
            if(attributes!=null && attributes.Length>0) {
                return (TAttribute)attributes[0];
            }
            else {
                return null;
            }
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this object obj, bool inherit = true)
            where TAttribute: Attribute {
            if (obj is Type) {
                var attrs = (obj as Type).GetCustomAttributes(typeof(TAttribute), inherit);
                if (attrs != null) {
                    return attrs.FirstOrDefault() as TAttribute;
                }
            }
            else if (obj is PropertyInfo) {
                var attrs = ((PropertyInfo)obj).GetCustomAttributes(inherit);
                if (attrs != null && attrs.Length > 0) {
                    return attrs.FirstOrDefault(attr => attr is TAttribute) as TAttribute;
                }
            }
            else if (obj is MethodInfo) {
                var attrs = (obj as MethodInfo).GetCustomAttributes(inherit);
                if (attrs != null && attrs.Length > 0) {
                    return attrs.FirstOrDefault(attr => attr is TAttribute) as TAttribute;
                }
            }
            else if (obj.GetType().IsDefined(typeof(TAttribute), true)) {
                var attr = Attribute.GetCustomAttribute(obj.GetType(), typeof(TAttribute), inherit) as TAttribute;
                return attr;
            }
            return null;
        }

        

        public static T GetValueByKey<T>(this object obj, string name) {
            T retValue = default(T);
            object objValue = null;
            try {
                var property = obj.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (property != null) {
                    objValue = FastInvoke.GetMethodInvoker(property.GetGetMethod(true))(obj, null);
                }

                if (objValue != null) {
                    retValue = (T)objValue;
                }
            }
            catch (Exception) {
                retValue = default(T);
            }
            return retValue;
        }

        public static object GetValueByKey(this object obj, string name) {
            object objValue = null;
            var property = obj.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (property != null) {
                objValue = FastInvoke.GetMethodInvoker(property.GetGetMethod(true))(obj, null);
                // objValue = property.GetValue(obj, null);
            }
            return objValue;
        }

        public static void SetValueByKey(this object obj, string name, object value) {
            var property = obj.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (property != null) {
                FastInvoke.GetMethodInvoker(property.GetSetMethod(true))(obj, new object[] { value });
            }
        }

        /// <summary>
        /// 把字符串解析成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string val) where T: struct {
            try {
                return (T)Enum.Parse(typeof(T), val);
            }
            catch (Exception) {
                return default(T);
            }
        }

        /// <summary> 
        /// 序列化 
        /// </summary> 
        /// <param name="data">要序列化的对象</param> 
        /// <returns>返回存放序列化后的数据缓冲区</returns> 
        public static byte[] ToBytes(this object data) {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream rems = new MemoryStream();
            formatter.Serialize(rems, data);
            return rems.GetBuffer();
        }

        /// <summary> 
        /// 反序列化 
        /// </summary> 
        /// <param name="data">数据缓冲区</param> 
        /// <returns>对象</returns> 
        public static object ToObject(this byte[] data) {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream rems = new MemoryStream(data);
            data = null;
            return formatter.Deserialize(rems);
        }
    }
}