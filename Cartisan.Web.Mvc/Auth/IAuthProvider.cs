namespace Cartisan.Web.Mvc.Auth {
    public interface IAuthProvider {
        bool Authenticate(string username, string password);
    }
}