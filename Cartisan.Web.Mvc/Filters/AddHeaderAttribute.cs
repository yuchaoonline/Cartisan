using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Filters {
    public class AddHeaderAttribute: ActionFilterAttribute {
        public string Name { get; set; }
        public string Value { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Value)) {
                filterContext.RequestContext.HttpContext.Response.AddHeader(Name, Value);
            }
        }
    }
}