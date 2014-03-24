using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cartisan.Infrastructure.Extensions {
    public static class JsonExtension {
        public static string ToJson(this object obj, bool serializeNonPublic = false,
            bool useCamelCasePropertyName = true, bool indented = false) {

            return JsonConvert.SerializeObject(obj, indented ? Formatting.Indented : Formatting.None,
                GetCustomJsonSerializersettings(serializeNonPublic, useCamelCasePropertyName));
        }

        public static object ToJsonObject(this string json, bool serializeNonPublic = false,
            bool useCamelCasePropertyName = true) {
            try {
                return JsonConvert.DeserializeObject(json,
                    GetCustomJsonSerializersettings(serializeNonPublic, useCamelCasePropertyName));
            }
            catch (Exception) {
                throw;
            }
        }

        private static JsonSerializerSettings _nonPublicSerializerSettings;
        private static JsonSerializerSettings NonPublicSerializerSettings {
            get {
                return _nonPublicSerializerSettings
                    ?? (_nonPublicSerializerSettings = CreateCustomerSerializerSettings(true, false));
            }
        }
        
        private static JsonSerializerSettings _nonPublicCamelCasePropertyNameSerializerSettings;
        private static JsonSerializerSettings NonPublicCamelCasePropertyNameSerializerSettings {
            get {
                return _nonPublicCamelCasePropertyNameSerializerSettings
                    ?? (_nonPublicCamelCasePropertyNameSerializerSettings = CreateCustomerSerializerSettings(true, true));
            }
        }

        private static JsonSerializerSettings _publicSerializerSettings;
        private static JsonSerializerSettings PublicSerializerSettings {
            get {
                return _publicSerializerSettings
                    ?? (_publicSerializerSettings = CreateCustomerSerializerSettings(false, false));
            }
        }

        private static JsonSerializerSettings _publicCamelCasePropertyNameSerializerSettings;
        private static JsonSerializerSettings PublicCamelCasePropertyNameSerializerSettings {
            get {
                return _publicCamelCasePropertyNameSerializerSettings
                    ?? (_publicCamelCasePropertyNameSerializerSettings = CreateCustomerSerializerSettings(false, true));
            }
        }

        private static JsonSerializerSettings CreateCustomerSerializerSettings(bool serializeNonPublic,
            bool useCamelCasePropertyName) {
            return new JsonSerializerSettings() {
                ContractResolver = new CartisanContractResolver(serializeNonPublic,
                    useCamelCasePropertyName)
            };
        }


        private static JsonSerializerSettings GetCustomJsonSerializersettings(bool serializeNonPublic,
            bool useCamelCasePropertyName) {
            if(serializeNonPublic) {
                if(useCamelCasePropertyName) {
                    return NonPublicCamelCasePropertyNameSerializerSettings;
                }
                else {
                    return NonPublicSerializerSettings;
                }
            }
            else {
                if(useCamelCasePropertyName) {
                    return PublicCamelCasePropertyNameSerializerSettings;
                }
                else {
                    return PublicSerializerSettings;
                }
            }
        }
    }

    internal class CartisanContractResolver: DefaultContractResolver {
        private readonly bool _useCamelCasePropertyName;

        public CartisanContractResolver(bool serializeNonPublic, bool useCamelCasePropertyName): base(false) {
            this._useCamelCasePropertyName = useCamelCasePropertyName;
            if (serializeNonPublic) {
                this.DefaultMembersSearchFlags |= BindingFlags.NonPublic;
            }
        }

        protected override string ResolvePropertyName(string propertyName) {
            if(_useCamelCasePropertyName) {
                return propertyName.ToCamelCase();
            }
            return propertyName;
        }
    }
}