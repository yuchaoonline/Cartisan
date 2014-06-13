using System;
using Cartisan.IoC;

namespace Cartisan.Log {
    /// <summary>
    /// 系统日志工厂
    /// </summary>
    public static class LoggerFactory {
        private static readonly ILoggerFactoryAdapter LoggerFactoryAdapter = ServiceLocator.GetService<ILoggerFactoryAdapter>();

        /// <summary>
        /// 根据LoggerName获取ILogger
        /// </summary>
        /// <param name="loggerName"></param>
        /// <returns></returns>
        public static ILogger GetLogger(string loggerName) {
            return LoggerFactoryAdapter.GetLogger(loggerName);
        }

        /// <summary>
        /// 根据LoggerType获取ILogger
        /// </summary>
        /// <param name="loggerType"></param>
        /// <returns></returns>
        public static ILogger GetLogger(Type loggerType) {
            return LoggerFactoryAdapter.GetLogger(loggerType);
        }

        /// <summary>
        /// 获取ILogger
        /// </summary>
        /// <returns></returns>
        public static ILogger GetLogger() {
            return GetLogger("Cartisan");
        }
    }
}