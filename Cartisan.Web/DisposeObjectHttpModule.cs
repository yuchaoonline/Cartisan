using System;
using System.Collections;
using System.Web;

namespace Cartisan.Web {
    public class DisposeObjectHttpModule: IHttpModule {
        public void Init(HttpApplication context) {
            context.EndRequest+=context_EndRequest;
        }

        private void context_EndRequest(object sender, EventArgs e) {
            if(HttpContext.Current!=null) {
                foreach(DictionaryEntry entry in HttpContext.Current.Items) {
                    if(entry.Value!=null && entry.Value is IDisposable) {
                        (entry.Value as IDisposable).Dispose();
                    }
                }
            }
        }

        public void Dispose() {
        }
    }
}