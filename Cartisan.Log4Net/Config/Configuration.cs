using Cartisan.Infrastructure;
using Cartisan.Infrastructure.Log;
using Cartisan.IoC;

namespace Cartisan.Log4Net.Config {
    public static class ConfigurationExtension {
        public static void UseLog4Net() {
            UseLog4Net("log4net.config");
        }

        public static void UseLog4Net(string configFile) {
            ServiceLocator.GetService<ILoggerFactory>();
        }
    }
}