using System;
using System.IO;
using System.Net;
using System.Web;

namespace Cartisan.Web {
    public class DefaultRunningEnvironment: IRunningEnvironment {
        private const string WebConfigPath = "~/web.config";
        private const string BinPath = "~/bin";
        private const string RefreshHtmlPath = "~/refresh.html";

        public bool IsFullTrust {
            get {
                if(AppDomain.CurrentDomain.IsHomogenous) {
                    return AppDomain.CurrentDomain.IsFullyTrusted;
                }
                else {
                    return false;
                }
            }
        }

        public void RestartAppDomain() {
            if(this.IsFullTrust) {
                HttpRuntime.UnloadAppDomain();
            }
            else if(!this.TryWriteBinFolder() && !this.TryWriteWebConfig()) {
                throw new ApplicationException(string.Format("需要启动站点，在非FullTrust环境下必须给\"{0}\"或者\"~/{1}\"写入的权限", "~/bin",
                    "~/web.config"));
            }

            HttpContext httpContext = HttpContext.Current;
            if(httpContext==null) {
                return;
            }
            if(httpContext.Request.RequestType=="GET") {
                httpContext.Response.Redirect(httpContext.Request.RawUrl, true);
            }
            else {
                httpContext.Response.ContentType = "text/html";
                httpContext.Response.WriteFile("~/refresh.html");
                httpContext.Response.End();
            }
        }

        // 尝试修改web.config最后更新时间，目的是使应用程序自动重新加载
        private bool TryWriteWebConfig() {
            try {
                File.SetLastWriteTimeUtc(WebUtility.GetPhysicalFilePath("~/web.config"), DateTime.UtcNow);
                return true;
            }
            catch(Exception) {
                return false;
            }
        }

        // 尝试引起bin文件夹的改动，目的是使应用程序自动重新加载
        private bool TryWriteBinFolder() {
            try {
                string binHostPath = WebUtility.GetPhysicalFilePath("~/binHostRestart");
                Directory.CreateDirectory(binHostPath);
                using(StreamWriter writer = File.CreateText(Path.Combine(binHostPath, "log.txt"))) {
                    writer.WriteLine("Restart on '{0}'", DateTime.UtcNow);
                    writer.Flush();
                }
                return true;
            }
            catch(Exception) {
                return false;
            }
        }
    }
}