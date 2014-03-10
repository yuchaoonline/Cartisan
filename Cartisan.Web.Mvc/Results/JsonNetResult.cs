using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cartisan.Web.Mvc.Results {
    public class JsonNetResult: JsonResult {
        public bool UseCamelCasePropertyName { get; set; }
        public Formatting Formatting { get; set; }

        public JsonNetResult() {
            this.UseCamelCasePropertyName = true;
            this.Formatting = Formatting.None;
        }

        public override void ExecuteResult(ControllerContext context) {
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
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
                if(this.UseCamelCasePropertyName) {
                    serializerSettings.ContractResolver=new CamelCasePropertyNamesContractResolver();
                }

                JsonTextWriter writer = new JsonTextWriter(response.Output) {
                    Formatting = this.Formatting
                };

                JsonSerializer serializer = JsonSerializer.Create(serializerSettings);
                serializer.Serialize(writer, this.Data);
                writer.Flush();
            }
        }
    }
}