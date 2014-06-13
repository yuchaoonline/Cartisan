using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

namespace Cartisan.Web {
    /// <summary>
    /// 提供与Web请求时可使用的工具类，包括Url解析、Url/Html编码、获取IP地址、返回Http状态码
    /// </summary>
    public static class WebUtility {
        public const string HtmlNewLine = "<br />";

        /// <summary>
        /// 将Url转换为在请求客户端可用的Url（转换 ~/ 为绝对路径）
        /// </summary>
        /// <param name="relativeUrl">相对Url</param>
        /// <returns></returns>
        public static string ResolveUrl(string relativeUrl) {
            if (string.IsNullOrEmpty(relativeUrl) || !relativeUrl.StartsWith("~/")) {
                return relativeUrl;
            }

            string[] strings = relativeUrl.Split('?');

            string url = VirtualPathUtility.ToAbsolute(strings[0]);

            if(strings.Length>1) {
                url = url + "?" + strings[1];
            }

            return url;
        }

        /// <summary>
        /// 获取带传输协议的完整的主机地址
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string HostPath(Uri uri) {
            if(uri==null) {
                return string.Empty;
            }

            string port = uri.IsDefaultPort
                ? string.Empty
                : ":" + Convert.ToString(uri.Port, CultureInfo.InvariantCulture);

            return uri.Scheme + Uri.SchemeDelimiter + uri.Host + port;
        }

        /// <summary>
        /// 获取物理文件路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <remarks>
        /// <para>filePath支持以下格式</para>
        /// <list type="bullet">
        /// <item>~/abc/</item>
        /// <item>c:\abc\</item>
        /// <item>\\192.168.0.1\abc\</item>
        /// </list>
        /// </remarks>
        /// <returns></returns>
        public static string GetPhysicalFilePath(string filePath) {
            string path;
            if(filePath.IndexOf(":\\", StringComparison.Ordinal)!=-1 || 
                filePath.IndexOf("\\\\", StringComparison.Ordinal)!=-1) {
                path = filePath;
            }
            else if(HostingEnvironment.IsHosted) {
                path = HostingEnvironment.MapPath(filePath);
            }
            else {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    filePath.Replace('/', Path.DirectorySeparatorChar).Replace("~", ""));
            }

            return path;
        }

        /// <summary>
        /// 把content中的虚拟路径转化成完整的url
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FormatCompleteUrl(string content) {
            const string pattern1 = "src=[\"']\\s*(/[^\"']*)\\s*[\"']";
            const string pattern2 = "href=[\"']\\s*(/[^\"']*)\\s*[\"']";
            string path = HostPath(HttpContext.Current.Request.Url);
            content = Regex.Replace(content, pattern1, "src=\"" + path + "$1\"",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            content = Regex.Replace(content, pattern2, "href=\"" + path + "$1\"",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return content;
        }

        /// <summary>
        /// 获取根域名
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="domainRules">域名规则</param>
        /// <returns></returns>
        public static string GetServerDomain(Uri uri, string[] domainRules) {
            if(uri==null) {
                return string.Empty;
            }

            string host = uri.Host.ToLower();
            if(host.IndexOf('.')<=0) {
                return host;
            }

            string[] strings = host.Split('.');
            int result = -1;
            if(int.TryParse(strings[strings.Length-1],out result)) {
                return host;
            }

            foreach(string domainRule in domainRules) {
                if(host.EndsWith(domainRule.ToLower())) {
                    string ruleResult = host.Replace(domainRule.ToLower(), "");
                    if(ruleResult.IndexOf('.')<=0) {
                        return ruleResult + domainRule;
                    }

                    strings = ruleResult.Split('.');
                    return strings[strings.Length - 1] + domainRule;
                }
            }

            return host;
        }

        /// <summary>
        /// Html编码
        /// </summary>
        /// <param name="rawContent"></param>
        /// <returns></returns>
        public static string HtmlEncode(string rawContent) {
            if(string.IsNullOrEmpty(rawContent)) {
                return rawContent;
            }
            else {
                return HttpUtility.HtmlEncode(rawContent);
            }
        } 
        
        /// <summary>
        /// Html解码
        /// </summary>
        /// <param name="rawContent"></param>
        /// <returns></returns>
        public static string HtmlDecode(string rawContent) {
            if(string.IsNullOrEmpty(rawContent)) {
                return rawContent;
            }
            else {
                return HttpUtility.HtmlDecode(rawContent);
            }
        }

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="urlToEncode"></param>
        /// <returns></returns>
        public static string UrlEncode(string urlToEncode) {
            if(string.IsNullOrEmpty(urlToEncode)) {
                return urlToEncode;
            }
            else {
                return HttpUtility.UrlEncode(urlToEncode).Replace("'", "%27");
            }
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP() {
            return GetIP(HttpContext.Current);
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP(HttpContext httpContext) {
            string ip = string.Empty;
            
            if(httpContext==null) {
                return ip;
            }

            ip = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if(string.IsNullOrEmpty(ip)) {
                ip = httpContext.Request.ServerVariables["REMOTE_ADDR"];
            }
            if(string.IsNullOrEmpty(ip)) {
                ip = httpContext.Request.UserHostAddress;
            }

            return ip;
        }

        /// <summary>
        /// 返回 StatusCode 404
        /// </summary>
        public static void Return404() {
            Return404(HttpContext.Current);
        }

        /// <summary>
        /// 返回 StatusCode 404
        /// </summary>
        public static void Return404(HttpContext httpContext) {
            if (httpContext != null) {
                ReturnStatusCode(httpContext, 404, null, false);
                httpContext.Response.SuppressContent = true;
                httpContext.Response.End();
            }
        }

        /// <summary>
        /// 返回 StatusCode 403
        /// </summary>
        public static void Return403() {
            Return403(HttpContext.Current);
        }

        /// <summary>
        /// 返回 StatusCode 403
        /// </summary>
        public static void Return403(HttpContext httpContext) {
            if(httpContext!=null) {
                ReturnStatusCode(httpContext, 403, null, false);
                httpContext.Response.SuppressContent = true;
                httpContext.Response.End();
            }
        }

        /// <summary>
        /// 返回 StatusCode 304
        /// </summary>
        public static void Return304() {
            Return304(HttpContext.Current);
        }

        /// <summary>
        /// 返回 StatusCode 304
        /// </summary>
        public static void Return304(HttpContext httpContext) {
            ReturnStatusCode(httpContext, 304, "304 Not Modified", false);
        }

        /// <summary>
        /// 返回Http状态码
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="statusCode">状态码</param>
        /// <param name="status">状态描述字符串</param>
        /// <param name="endResponse"></param>
        public static void ReturnStatusCode(HttpContext httpContext, int statusCode, string status, bool endResponse) {
            if(httpContext!=null) {
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = statusCode;
                if(!string.IsNullOrEmpty(status)) {
                    httpContext.Response.Status = status;
                }
                if(endResponse) {
                    httpContext.Response.End();
                }
            }
        }
    }
}