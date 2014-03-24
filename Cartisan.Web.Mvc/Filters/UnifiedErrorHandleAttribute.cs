using System.Net;
using System.Text;
using System.Web.Mvc;
using Cartisan.Infrastructure;
using Cartisan.Web.Mvc.Results;

namespace Cartisan.Web.Mvc.Filters {
    public class UnifiedErrorHandleAttribute: HandleErrorAttribute {
        public override void OnException(ExceptionContext filterContext) {
            string errorMessage = filterContext.Exception.Message;
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            CartisanException cartisanException = filterContext.Exception as CartisanException;

            string status = cartisanException == null ? ResultStatus.Exception :
                cartisanException.ErrorCode == ErrorCode.Unauthorized ? ResultStatus.Unauthorized :
                    cartisanException.ErrorCode == ErrorCode.ValidateFailure ? ResultStatus.ValidateFailure :
                        cartisanException.ErrorCode == ErrorCode.RuntimeFailure ? ResultStatus.RuntimeFailure :
                            ResultStatus.Exception;

            filterContext.Result = new JsonNetResult() {
                Data = new Result() {
                    Success = false,
                    Status = status,
                    Message = errorMessage
                },
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            filterContext.ExceptionHandled = true;
        }
    }
}