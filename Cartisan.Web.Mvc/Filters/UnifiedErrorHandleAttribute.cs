using System.Net;
using System.Text;
using System.Web.Mvc;
using Cartisan.Exceptions;
using Cartisan.Infrastructure;
using Cartisan.Web.Mvc.Results;

namespace Cartisan.Web.Mvc.Filters {
    public class UnifiedErrorHandleAttribute: HandleErrorAttribute {
        public override void OnException(ExceptionContext filterContext) {
            string errorMessage = filterContext.Exception.Message;
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ResultState status = filterContext.Exception is UnauthorizedException ? ResultState.Unauthorized :
                filterContext.Exception is ValidateFailureException ? ResultState.ValidateFailure :
                    filterContext.Exception is RuntimeFailureException ? ResultState.RuntimeFailure :
                        ResultState.Exception;

            filterContext.Result = new JsonNetResult() {
                Data = new ResponseResult() {
                    Success = false,
                    Status = status.ToString(),
                    Message = errorMessage
                },
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            filterContext.ExceptionHandled = true;
        }
    }
}