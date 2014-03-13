using System.Web.Security;

namespace Cartisan.Web.Mvc.Auth {
    public class FormAuthProvider: IAuthProvider {
        public bool Authenticate(string username, string password) {
            bool result = Membership.ValidateUser(username, password);

            if(result) {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return result;
        }
    }
}