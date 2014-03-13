using System;
using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Results {
    public class HttpStatusCodeResult: ActionResult {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public HttpStatusCodeResult(int statusCode): this(statusCode, null) {}

        public HttpStatusCodeResult(int statusCode, string statusDescription) {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public override void ExecuteResult(ControllerContext context) {
            if(context==null) {
                throw new ArgumentNullException("context");
            }

            context.HttpContext.Response.StatusCode = StatusCode;
            if(!string.IsNullOrEmpty(StatusDescription)) {
                context.HttpContext.Response.StatusDescription = StatusDescription;
            }
        }
    }
}