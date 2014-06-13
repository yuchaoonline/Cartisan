using Cartisan.Infrastructure;
using Cartisan.IoC;
using Cartisan.Log;

namespace Cartisan.Log4Net.Config {
    public static class ConfigurationExtension {
        public static void UseLog4Net() {
            UseLog4Net("log4net.config");
        }

        public static void UseLog4Net(string configFile) {
            ServiceLocator.GetService<ILoggerFactoryAdapter>();
        }
    }
}