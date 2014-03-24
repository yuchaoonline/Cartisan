using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Infrastructure.Utility {
    public static class ReflectionUtils {
        public static readonly Type[] EmptyTypes;

        static ReflectionUtils() {
            EmptyTypes = Type.EmptyTypes;
        }

        public static MethodInfo GetBaseDefinition(PropertyInfo propertyInfo) {
            ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");

            MethodInfo getMethod = propertyInfo.GetGetMethod();
            if (getMethod != null) {
                return getMethod.GetBaseDefinition();
            }
            MethodInfo setMethod = propertyInfo.GetSetMethod();
            if (setMethod != null) {
                return setMethod.GetBaseDefinition();
            }

            return null;
        }

        public static bool IsVirtual(PropertyInfo property) {
            ValidationUtils.ArgumentNotNull(property, "property");

            if (property.GetGetMethod() != null && property.GetGetMethod().IsVirtual) {
                return true;
            }
            if (property.GetSetMethod() != null && property.GetSetMethod().IsVirtual) {
                return true;
            }
            return false;
        }

        public static bool IsPublic(PropertyInfo property) {
            ValidationUtils.ArgumentNotNull(property, "property");
            if (property.GetGetMethod() != null && property.GetGetMethod().IsPublic) {
                return true;
            }
            if (property.GetSetMethod() != null && property.GetSetMethod().IsPublic) {
                return true;
            }
            return false;
        }

        public static MemberInfo GetMemberInfoFromType(Type type, MemberInfo memberInfo) {
            const BindingFlags bindingAttribute = BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic;

            switch(memberInfo.MemberType) {
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)memberInfo;

                    Type[] types = propertyInfo.GetIndexParameters().Select(p => p.ParameterType).ToArray();
                    return type.GetProperty(propertyInfo.Name, bindingAttribute, null, propertyInfo.PropertyType, types,
                        null);
                default:
                    return type.GetMember(memberInfo.Name, memberInfo.MemberType, bindingAttribute).SingleOrDefault();

            }
        }

        public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttribute) {
            List<MemberInfo> targetMembers = new List<MemberInfo>();

            targetMembers.AddRange(GetFields(type, bindingAttribute));
            targetMembers.AddRange(GetProperties(type, bindingAttribute));

            List<MemberInfo> distinctMembers = new List<MemberInfo>(targetMembers.Count);

            foreach(var groupedMembber in targetMembers.GroupBy(m=>m.Name)) {
                int count = groupedMembber.Count();
                IList<MemberInfo> members = groupedMembber.ToList();

                if(count==1) {
                    distinctMembers.Add(members.First());
                }
                else {
                    IList<MemberInfo> resolvedMembers = new List<MemberInfo>();
                    foreach(MemberInfo memberInfo in resolvedMembers) {
                        if(resolvedMembers.Count==0) {
                            resolvedMembers.Add(memberInfo);
                        }
                        else if(!IsOverridenGenericMember(memberInfo, bindingAttribute) || memberInfo.Name=="Item") {
                            resolvedMembers.Add(memberInfo);
                        }
                    }

                    distinctMembers.AddRange(resolvedMembers);
                }
            }

            return distinctMembers;
        }

        private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttribute) {
            if(memberInfo.MemberType!=MemberTypes.Property) {
                return false;
            }

            PropertyInfo property = (PropertyInfo)memberInfo;
            if(!IsVirtual(property)) {
                return false;
            }

            Type declaringType = property.DeclaringType;
            if(!declaringType.IsGenericType) {
                return false;
            }

            Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
            if(genericTypeDefinition==null) {
                return false;
            }

            MemberInfo[] members = genericTypeDefinition.GetMember(property.Name, bindingAttribute);
            if(members.Length==0) {
                return false;
            }

            Type memberUnderlyingType = GetMemberUnderlyingType(members[0]);
            if(!memberUnderlyingType.IsGenericParameter) {
                return false;
            }

            return true;
        }

        public static Type GetMemberUnderlyingType(MemberInfo member) {
            ValidationUtils.ArgumentNotNull(member, "member");

            switch(member.MemberType) {
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                default:
                    throw new ArgumentException("member 必须是 FieldInfo、PropertyInfo、EventInfo 三个类型之一。");
            }
        }

        public static IEnumerable<FieldInfo> GetFields(Type type, BindingFlags bindingAttribute) {
            ValidationUtils.ArgumentNotNull(type, "type");

            List<FieldInfo> fieldInfos = type.GetFields(bindingAttribute).ToList();

            GetChildPrivateFields(fieldInfos, type, bindingAttribute);

            return fieldInfos;
        }

        private static void GetChildPrivateFields(IList<FieldInfo> initialFields, Type type,
            BindingFlags bindingAttribute) {
            if ((bindingAttribute & BindingFlags.NonPublic) != 0) {
                // 不搜索 public 的 field
                BindingFlags nonPublicBindingAttribute = bindingAttribute.RemoveFlag(BindingFlags.Public);

                while ((type = type.BaseType) != null) {
                    // 过滤掉 protected 的 field
                    initialFields.AddRange(type.GetFields(nonPublicBindingAttribute).Where(field => field.IsPrivate));
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetProperties(Type type, BindingFlags bindingAttribute) {
            ValidationUtils.ArgumentNotNull(type, "type");

            List<PropertyInfo> propertyInfos = type.GetProperties(bindingAttribute).ToList();
            GetChildPrivateProperties(propertyInfos, type, bindingAttribute);

            for(int i = 0; i < propertyInfos.Count; i++) {
                PropertyInfo propertyInfo = propertyInfos[i];
                if(propertyInfo.DeclaringType != type) {
                    propertyInfos[i] = (PropertyInfo)GetMemberInfoFromType(propertyInfo.DeclaringType, propertyInfo);
                }
            }

            return propertyInfos;
        }

        private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type type,
            BindingFlags bindingAttribute) {
            while ((type = type.BaseType) != null) {
                foreach (PropertyInfo propertyInfo in type.GetProperties(bindingAttribute)) {
                    if (!IsPublic(propertyInfo)) {
                        int index = initialProperties.IndexOf(p => p.Name == propertyInfo.Name);
                        if (index == -1) {
                            initialProperties.Add(propertyInfo);
                        }
                        else {
                            PropertyInfo childProperty = initialProperties[index];
                            if (!IsPublic(childProperty)) {
                                initialProperties[index] = propertyInfo;
                            }
                        }
                    }
                    else {
                        if (!IsVirtual(propertyInfo)) {
                            int index = initialProperties.IndexOf(p => p.Name == propertyInfo.Name
                                && p.DeclaringType == propertyInfo.DeclaringType);
                            if (index == -1) {
                                initialProperties.Add(propertyInfo);
                            }
                        }
                        else {
                            int index = initialProperties.IndexOf(p => p.Name == propertyInfo.Name
                                && IsVirtual(p) && GetBaseDefinition(p) != null
                                && GetBaseDefinition(p).DeclaringType.IsAssignableFrom(propertyInfo.DeclaringType));
                            if (index == -1) {
                                initialProperties.Add(propertyInfo);
                            }
                        }
                    }
                }
            }
        }

        public static BindingFlags RemoveFlag(this BindingFlags bindingAttribute, BindingFlags flag) {
            return ((bindingAttribute & flag) == flag) ? bindingAttribute ^ flag : bindingAttribute;
        }

        
    }
}