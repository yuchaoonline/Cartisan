using System;
using System.Web.Mvc;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Web.Mvc.Results {
    public class JsonpResult: JsonNetResult {
        private const String _jsonpCallbackName = "callback";

        protected override string GetJsonString(ControllerContext context, object data) {
            if (context.HttpContext.Request[_jsonpCallbackName] == null) {
                return data.ToJson();
            }
            else {
                return string.Format("{0}({1})", context.HttpContext.Request[_jsonpCallbackName], data.ToJson());
            }
        }
    }
}