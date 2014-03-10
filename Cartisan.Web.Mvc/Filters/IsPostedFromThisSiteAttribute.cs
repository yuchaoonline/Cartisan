using System.Web;
using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Filters {
    public class IsPostedFromThisSiteAttribute: AuthorizeAttribute {
        public override void OnAuthorization(AuthorizationContext filterContext) {
            if(filterContext.HttpContext!=null) {
                if(filterContext.HttpContext.Request.UrlReferrer==null) {
                    throw new HttpException("提交无效");
                }
                // TODO：获取当前站点
                if(filterContext.HttpContext.Request.UrlReferrer.Host!="mysite.com") {
                    throw new HttpException("不是从本站提交的表单");
                }
            }
        }
    }
}