using System;
using System.Web;
using System.Web.Mvc;
using Cartisan.Infrastructure.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cartisan.Web.Mvc.Results {
    public class JsonpResult: JsonNetResult {
        private const String JsonpCallbackName = "callback";
        public override void ExecuteResult(ControllerContext context) {
            if(context.HttpContext.Request[JsonpCallbackName]==null) {
                base.ExecuteResult(context);
                return;
            }

            if(context ==null) {
                throw new ArgumentException("context");
            }

            if(this.JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)) {
                throw new InvalidOperationException("禁止使用GET请求，要允许GET请求，请将JsonRequestBehavior设置为AllowGet。");
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

            if(this.ContentEncoding!=null) {
                response.ContentEncoding = this.ContentEncoding;
            }

            if(this.Data!=null) {
                response.Write(string.Format("{0}({1})", context.HttpContext.Request[JsonpCallbackName],
                    this.Data.ToJson()));
            }
        }
    }
}