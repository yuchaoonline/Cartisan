using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cartisan.Web.Mvc.Providers {
    public class JsonNetValueProviderFactory: ValueProviderFactory {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext) {
            if(controllerContext==null) {
                throw new ArgumentException("controllerContext");
            }

            if(!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json",
                    StringComparison.OrdinalIgnoreCase)) {
                return null;
            }

            object jsonObject;

            using(StreamReader streamReader = new StreamReader(controllerContext.HttpContext.Request.InputStream)) {
                using(JsonTextReader reader = new JsonTextReader(streamReader)) {
                    if(!reader.Read()) {
                        return null;
                    }

                    JsonSerializer jsonSerializer = new JsonSerializer();
                    jsonSerializer.Converters.Add(new ExpandoObjectConverter());

                    if(reader.TokenType==JsonToken.StartArray) {
                        jsonObject = jsonSerializer.Deserialize<List<ExpandoObject>>(reader);
                    }
                    else {
                        jsonObject = jsonSerializer.Deserialize<ExpandoObject>(reader);
                    }
                }
            }

            Dictionary<string, object> backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            AddToBackingStore(backingStore, string.Empty, jsonObject);

            return new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
        }

        private void AddToBackingStore(Dictionary<string, object> backingStore, string prefix, object value) {
            IDictionary<string, object> d = value as IDictionary<string, object>;
            if(d!=null) {
                foreach(KeyValuePair<string, object> entry in d) {
                    this.AddToBackingStore(backingStore, MakePropertyKey(prefix, entry.Key), entry.Value);
                }
                return;
            }

            IList l = value as IList;
            if(l!=null) {
                for(int i = 0; i < l.Count; i++) {
                    this.AddToBackingStore(backingStore, MakeArrayKey(prefix, i), l[i]);
                }
            }
        }

        private string MakeArrayKey(string prefix, int index) {
            return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
        }

        private string MakePropertyKey(string prefix, string propertyName) {
            return string.IsNullOrEmpty(prefix) ? propertyName : prefix + "." + propertyName;
        }
    }
}