using System;
using System.Web;
using System.Web.Mvc;
using Cartisan.Infrastructure.Extensions;
using Cartisan.Infrastructure.Utility;
using Newtonsoft.Json;

namespace Cartisan.Web.Mvc.Results {
    public class JsonNetResult: JsonResult {
        public bool UseCamelCasePropertyName { get; set; }
        public Formatting Formatting { get; set; }

        public JsonNetResult() {
            this.UseCamelCasePropertyName = true;
            this.Formatting = Formatting.None;
        }

        public override void ExecuteResult(ControllerContext context) {
            ValidationUtils.ArgumentNotNull(context, "context");

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
                response.Write(this.GetJsonString(context, this.Data));
            }
        }

        protected virtual string GetJsonString(ControllerContext context, object data) {
            return data.ToJson();
        }
    }
}