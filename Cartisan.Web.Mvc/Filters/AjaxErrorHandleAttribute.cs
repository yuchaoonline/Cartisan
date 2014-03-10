using System.Net;
using System.Text;
using System.Web.Mvc;
using Cartisan.Web.Mvc.Results;

namespace Cartisan.Web.Mvc.Filters {
    public class AjaxErrorHandleAttribute: HandleErrorAttribute {
        public override void OnException(ExceptionContext filterContext) {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest()) {
                var errorMessage = filterContext.Exception.Message;
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                /*var status = filterContext.Exception is UnauthorizedException ? ResultState.Unauthorized :
                    filterContext.Exception is ValidateFailureException ? ResultState.ValidateFailure :
                        filterContext.Exception is RuntimeFailureException ? ResultState.RuntimeFailure :
                            ResultState.Exception;*/

                filterContext.Result = new JsonNetResult() {
                    Data = new ResponseResult() {
                        Success = false,
                        Status = "",
                        Message = errorMessage
                    },
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else {
                filterContext.ExceptionHandled = true;
            }
        }
    }
}