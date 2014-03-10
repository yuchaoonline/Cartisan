using System;
using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace Cartisan.Infrastructure {
    public class DynamicJson: DynamicObject {
        private JObject _json;

        public DynamicJson(JObject json) {
            this._json = json;
        }

        public JObject Json {
            get { return this._json; }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            bool ret = false;
            JToken value;
            if(this.Json.TryGetValue(binder.Name, out value)) {
                result = (value as JValue).Value;
                ret = true;
            }
            else {
                result = null;
            }
            return ret;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value) {
            bool ret = true;
            try {
                var prototype = this.Json.Property(binder.Name);
                if(prototype!=null) {
                    prototype.Value = JToken.FromObject(value);
                }
                else {
                    this.Json.Add(binder.Name, new JObject(value));
                }
            }
            catch(Exception) {
                ret = false;
            }

            return ret;
        }
    }
}