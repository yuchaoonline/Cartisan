using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Filters {
    public class CompressAttribute: ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            HttpRequestBase request = filterContext.HttpContext.Request;
            CompressionScheme preferredEncoding = this.GetPreferredEncoding(request);

            HttpResponseBase response = filterContext.HttpContext.Response;

            if (preferredEncoding==CompressionScheme.Gzip) {
                //response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (preferredEncoding==CompressionScheme.Deflate) {
                //response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }

        private CompressionScheme GetPreferredEncoding(HttpRequestBase request) {
            string acceptableEncoding = request.Headers["Accept-Encoding"].ToLower();
            
            if(acceptableEncoding.Contains("gzip")) {
                return CompressionScheme.Gzip;
            }
            if(acceptableEncoding.Contains("deflate")) {
                return CompressionScheme.Deflate;
            }
            return CompressionScheme.Identity;
        }

        enum CompressionScheme {
            Gzip = 0,
            Deflate = 1,
            Identity = 2
        }
    }
}