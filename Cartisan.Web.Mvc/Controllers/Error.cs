using System.Web.Mvc;
using Cartisan.Web.Mvc.Results;
using HttpNotFoundResult = Cartisan.Web.Mvc.Results.HttpNotFoundResult;

namespace Cartisan.Web.Mvc.Controllers {
    public class Error: ControllerBase {
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

//        routes.MapRoute("404-catch-all", "{*catchall}",
//            new { controller = "Error", action = "NotFound" });
        public ActionResult NotFound() {
            return new HttpNotFoundResult();
        }
    }
}