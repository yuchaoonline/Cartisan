using System.Web.Mvc;
using Cartisan.Web.Mvc.Results;

namespace Cartisan.Web.Mvc.Filters {
    public class CartisanAuthorizeAttribute: AuthorizeAttribute {
        public override void OnAuthorization(AuthorizationContext filterContext) {
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            if(filterContext.RequestContext.HttpContext.Request.IsAjaxRequest()) {
                filterContext.Result = new JsonNetResult();
            }
            else {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}