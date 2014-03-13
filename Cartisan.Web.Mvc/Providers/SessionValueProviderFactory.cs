using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Providers {
    public class SessionValueProviderFactory: ValueProviderFactory {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext) {
            return new SessionValueProvider(controllerContext.HttpContext.Session);
        }
    }
}