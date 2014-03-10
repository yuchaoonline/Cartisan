using Cartisan.Infrastructure;
using Cartisan.Infrastructure.Log;

namespace Cartisan.Log4Net.Config {
    public static class ConfigurationExtension {
        public static void UseLog4Net() {
            UseLog4Net("log4net.config");
        }

        public static void UseLog4Net(string configFile) {

            IoCFactory.Resolve<ILoggerFactory>();
        }
    }
}