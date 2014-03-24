using System;
using System.Web;
using System.Web.Security;

namespace Cartisan.Web {
    public class FormsAuth {
        public void Submit() {
            FormsAuthentication.SetAuthCookie("username", false);

            FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(
                1, "username", DateTime.Now, DateTime.Now.AddMinutes(10), false, "userData");

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(ticket1));
            HttpContext.Current.Response.Cookies.Add(cookie1);

            // redict returnUrl
        } 
    }
}