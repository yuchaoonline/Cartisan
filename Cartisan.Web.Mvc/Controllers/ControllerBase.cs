using System;
using System.Text;
using System.Web.Mvc;
using Cartisan.Web.Mvc.Extensions;
using Cartisan.Web.Mvc.Filters;
using Cartisan.Web.Mvc.Results;

namespace Cartisan.Web.Mvc.Controllers {
    [Compress]
    [HandleError]
    public abstract class ControllerBase: Controller {
        protected virtual void PermanentRedirect(string url, bool endRequest) {
            Response.Clear();
            Response.StatusCode = 301;
            Response.AddHeader("Location", url);

            if(endRequest) {
                Response.End();
            }
        }

        protected override void OnException(ExceptionContext filterContext) {
            if(filterContext.Exception is InvalidOperationException) {
                filterContext.SwitchToErrorView();
            }
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) {
            return new JsonNetResult() {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}