using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Controllers {
    public class ErrorController: BaseController {
        // Catch-all route
        //routes.MapRoute(
        //"Catchall",
        //"{*anything}",
        //new { controller = "Error", action = "Missing" }
        //);
        public ActionResult Missing() {
            HttpContext.Response.StatusCode = 404;
            HttpContext.Response.TrySkipIisCustomErrors = true;

            // Log the error

            // Error View Model

            return this.View();
        }
    }
}