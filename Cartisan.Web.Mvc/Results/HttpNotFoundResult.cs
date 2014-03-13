using System;
using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Results {
    public class HttpNotFoundResult: HttpStatusCodeResult {
        public HttpNotFoundResult(): this(null) {}

        public HttpNotFoundResult(string statusDescription): base(404, statusDescription) {}

        public override void ExecuteResult(ControllerContext context) {
            base.ExecuteResult(context);
            //new ViewResult{ViewName = "NotFound"}.ExecuteResult(context);
        }
    }
}   