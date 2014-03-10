using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Extensions {
    public static class MvcContextExtension {
        public static void SwitchToErrorView(this ExceptionContext context, string view = "error", string master = "") {
            string controllerName = context.RouteData.Values["controller"] as string;
            string actionName = context.RouteData.Values["action"] as string;
            HandleErrorInfo model = new HandleErrorInfo(context.Exception, controllerName, actionName);
            var result = new ViewResult() {
                ViewName = view,
                MasterName = master,
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = context.Controller.TempData
            };
            context.Result = result;

            context.ExceptionHandled = true;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = 500;
            context.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}