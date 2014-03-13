using System.Collections.Generic;
using System.Web.Routing;

namespace Cartisan.Web.Mvc {
    public class ParamBuilder: ExplicitFacadeDictionary<string, object> {
        private readonly IDictionary<string, object> _wrapped = new Dictionary<string, object>(); 
        protected override IDictionary<string, object> Wrapped {
            get { return _wrapped; }
        }

        public static implicit operator RouteValueDictionary(ParamBuilder paramBuilder) {
            return new RouteValueDictionary(paramBuilder);
        }

        public ParamBuilder Username(string value) {
            _wrapped.Add("username", value);
            return this;
        }
    }
}