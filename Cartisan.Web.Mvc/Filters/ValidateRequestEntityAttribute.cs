using System.Linq;
using System.Web.Mvc;
using Cartisan.Infrastructure;

namespace Cartisan.Web.Mvc.Filters {
    public class ValidateRequestEntityAttribute: ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            ModelStateDictionary modelState = filterContext.Controller.ViewData.ModelState;

            if(!modelState.IsValid) {
                string errorMessages = string.Join("<br/>",
                    modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

                throw new CartisanException(ErrorCode.ValidateFailure, errorMessages);
            }
        }
    }
}