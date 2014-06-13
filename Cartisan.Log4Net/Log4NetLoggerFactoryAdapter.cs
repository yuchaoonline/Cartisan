using System;
using System.IO;
using Cartisan.IoC;
using Cartisan.Log;
using Cartisan.Web;
using log4net;
using log4net.Config;

namespace Cartisan.Log4Net {
    public class Log4NetLoggerFactoryAdapter: ILoggerFactoryAdapter {
        private static bool _isConfigLoaded;

        public Log4NetLoggerFactoryAdapter():this("~/Config/log4net.config") {
        }

        public Log4NetLoggerFactoryAdapter(string configFileName) {
            if(_isConfigLoaded) {
                return;
            }

            if(string.IsNullOrEmpty(configFileName)) {
                configFileName = "~/Config/log4net.config";
            }

            FileInfo configFile = new FileInfo(WebUtility.GetPhysicalFilePath(configFileName));
            if(!configFile.Exists) {
                throw new ApplicationException(string.Format("log4net配置文件 {0} 未找到。", configFile.FullName));
            }

            IRunningEnvironment runningEnvironment = ServiceLocator.GetService<IRunningEnvironment>();
            if(runningEnvironment.IsFullTrust) {
                XmlConfigurator.ConfigureAndWatch(configFile);
            }
            else {
                XmlConfigurator.Configure(configFile);
            }

            _isConfigLoaded = true;
        }

        public ILogger GetLogger(string name) {
            return new Log4NetLogger(LogManager.GetLogger(name));
        }

        public ILogger GetLogger(Type type) {
            return new Log4NetLogger(LogManager.GetLogger(type));
        }
    }
}