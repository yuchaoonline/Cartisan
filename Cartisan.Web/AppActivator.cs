//[assembly: WebActivator.PreApplicationStartMethod(
//typeof(SomeNamespace.AppActivator), "PreStart")]
//[assembly: WebActivator.PostApplicationStartMethod(
//typeof(SomeNamespace.AppActivator), "PostStart")]
//[assembly: WebActivator.ApplicationShutdownMethodAttribute(
//typeof(SomeNamespace.AppActivator), "Stop")]
namespace Cartisan.Web {

    public static class AppActivator {
        public static void PreStart() {
            // Code that runs before Application_Start.
        }
        public static void PostStart() {
            // Code that runs after Application_Start.
        }
        public static void Stop() {
            // Code that runs when the application is shutting down.
        }
    }
}