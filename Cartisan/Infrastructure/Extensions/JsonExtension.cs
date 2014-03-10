using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Cartisan.Infrastructure.Extensions {
    public static class JsonExtension {
        private static JsonSerializerSettings _nonPublicSerializerSettings;
        private static JsonSerializerSettings NonPublicSerializerSettings {
            get {
                if(_nonPublicSerializerSettings==null) {
                    DefaultContractResolver contractResolver = new ContractResolver();
                    _nonPublicSerializerSettings = new JsonSerializerSettings() {
                        ContractResolver = contractResolver
                    };
                }
                return _nonPublicSerializerSettings;
            }
        }

        private static readonly JsonSerializerSettings LoopSerizlizeSerializerSettings = new JsonSerializerSettings() {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
        };

        private static JsonSerializerSettings _nonPublicLoopSerizlizeSerizlizerSettings;
        private static JsonSerializerSettings NonPublicLoopSerizlizeSerizlizerSettings {
            get {
                if(_nonPublicLoopSerizlizeSerizlizerSettings==null) {
                    DefaultContractResolver contractResolver = new ContractResolver();
                    _nonPublicSerializerSettings = new JsonSerializerSettings() {
                        ContractResolver = contractResolver,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                        ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                    };
                }
                return _nonPublicLoopSerizlizeSerizlizerSettings;
            }
        }

        public static JsonSerializerSettings GetCustomJsonSerializersettings(bool serializeNonPublic, bool loopSerialize) {
            JsonSerializerSettings customSettings = null;
            if(serializeNonPublic && loopSerialize) {
                customSettings = NonPublicLoopSerizlizeSerizlizerSettings;
            }
            else if(serializeNonPublic) {
                customSettings = NonPublicSerializerSettings;
            }
            else if(loopSerialize) {
                customSettings = LoopSerizlizeSerializerSettings;
            }

            return customSettings;
        }

        public static string ToJson(this object obj, bool serializeNonPublic = false, bool loopSerialize = false) {
            return JsonConvert.SerializeObject(obj, GetCustomJsonSerializersettings(serializeNonPublic, loopSerialize));
        }

        public static object ToJsonObject(this string json, bool serializeNonPublic = true, bool loopSerialize = false) {
            try {
                return JsonConvert.DeserializeObject(json,
                    GetCustomJsonSerializersettings(serializeNonPublic, loopSerialize));
            }
            catch(Exception) {
                return null;
            }
        }

        public static object ToJsonObject(this string json, Type jsonType, bool serializeNonPublic = true,
            bool loopSerialize = false) {
            try {
                if(jsonType == typeof(List<dynamic>)) {
                    return json.ToDynamicObjects(serializeNonPublic, loopSerialize);
                }
                if(jsonType==typeof(object)) {
                    json.ToDynamicObject(serializeNonPublic, loopSerialize);
                }
                return JsonConvert.DeserializeObject(json, jsonType,
                    GetCustomJsonSerializersettings(serializeNonPublic, loopSerialize));
            }
            catch(Exception) {
                return null;
            }
        }

        public static T ToJsonObject<T>(this string json, bool serializeNonPublic = true, bool loopSerialize = false) {
            try {
                if(typeof(T)== typeof(List<dynamic>)) {
                    return (T)(Object)(json.ToDynamicObjects(serializeNonPublic, loopSerialize));
                }
                if(typeof(T)==typeof(object)) {
                    return json.ToDynamicObject(serializeNonPublic, loopSerialize);
                }
                return JsonConvert.DeserializeObject<T>(json,
                    GetCustomJsonSerializersettings(serializeNonPublic, loopSerialize));
            }
            catch(Exception) {
                return default(T);
            }
        }

        public static dynamic ToDynamicObject(this string json, bool serializeNonPublic = true,
            bool loopSerialize = false) {
            dynamic dynamicObject = null;
            JObject jsonObject = json.ToJsonObject<JObject>(serializeNonPublic, loopSerialize);
            if(jsonObject!=null) {
                dynamicObject = new DynamicJson(jsonObject);
            }

            return dynamicObject;
        }

        public static List<dynamic> ToDynamicObjects(this string json, bool serializeNonPublic = true,
            bool loopSerialize = false) {
            List<dynamic> dynamicObjects = null;
            JArray jsonObjects = json.ToJsonObject<JArray>(serializeNonPublic, loopSerialize);
            if(jsonObjects!=null) {
                dynamicObjects = new List<dynamic>();
                foreach(JToken jsonObject in jsonObjects) {
                    dynamicObjects.Add(new DynamicJson(jsonObject as JObject));
                }
            }

            return dynamicObjects;
        } 
    }

    internal class ContractResolver: DefaultContractResolver {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType) {
            return objectType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic).ToList();
        }
    }
}