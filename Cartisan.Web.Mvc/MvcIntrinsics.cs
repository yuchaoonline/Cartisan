using System.Web.Mvc;
using System.Web.WebPages;

namespace Cartisan.Web.Mvc {
    public static class MvcIntrinsics {
        public static HtmlHelper Html {
            get { return ((WebViewPage)WebPageContext.Current.Page).Html; }
        } 
        
        public static AjaxHelper Ajax {
            get { return ((WebViewPage)WebPageContext.Current.Page).Ajax; }
        } 
        
        public static UrlHelper Url {
            get { return ((WebViewPage)WebPageContext.Current.Page).Url; }
        } 
    }
}