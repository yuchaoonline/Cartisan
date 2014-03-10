using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Filters {
    public class AuthorizedOnlyAttribute: AuthorizeAttribute {
        public AuthorizedOnlyAttribute() {
            View = "error";
            Master = string.Empty;
        }

        public string View { get; set; }
        public string Master { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext) {
            base.OnAuthorization(filterContext);
            CheckIfUserIsAuthenticated(filterContext);
        }

        private void CheckIfUserIsAuthenticated(AuthorizationContext filterContext) {
            if(filterContext.Result==null) {
                return;
            }

            if(filterContext.HttpContext.Request.IsAjaxRequest()) {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.HttpContext.Response.End();
            }

            if(filterContext.HttpContext.User.Identity.IsAuthenticated) {
                if(string.IsNullOrEmpty(View)) {
                    return;
                }
                ViewResult result = new ViewResult() {ViewName = View, MasterName = Master};
                filterContext.Result = result;
            }
        }
    }
}