using System.Web.Mvc;
using Cartisan.Infrastructure;

namespace Cartisan.Web.Mvc.Filters {
    public class UnifiedJsonResponseAttribute: ActionFilterAttribute {
        public override void OnResultExecuting(ResultExecutingContext filterContext) {
            if(filterContext.Result is JsonResult) {
                JsonResult result = filterContext.Result as JsonResult;
                object data = result.Data;

                ResponseResult responseResult = new ResponseResult() {
                    Success = true,
                    Status = ResultState.Success.ToString(),
                    Data = data
                };

                result.Data = responseResult;
            }
        }
    }
}