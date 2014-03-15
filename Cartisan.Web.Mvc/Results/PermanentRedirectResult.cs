using System;
using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Results {
    public class PermanentRedirectResult: ViewResult {
        public string Url { get; set; }

        public PermanentRedirectResult(string url) {
            if(string.IsNullOrEmpty(url)) {
                throw new ArgumentException("url is null or empty", url);
            }
            this.Url = url;
        }

        public override void ExecuteResult(ControllerContext context) {
            if(context==null) {
                throw new ArgumentException("context");
            }

            context.HttpContext.Response.StatusCode = 301;
            context.HttpContext.Response.RedirectLocation = Url;
            context.HttpContext.Response.End();
        }
    }
}