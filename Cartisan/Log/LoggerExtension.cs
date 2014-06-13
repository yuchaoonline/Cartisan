using System;

namespace Cartisan.Log {
    public static class LoggerExtension {
        /// <summary>
        /// 记录Debug级别日志
        /// </summary>
        public static void Debug(this ILogger logger, object message) {
            logger.Log(LogLevel.Debug, message);
        }

        /// <summary>
        /// 记录Debug级别日志
        /// </summary>
        public static void Debug(this ILogger logger, object message, Exception exception) {
            logger.Log(LogLevel.Debug, message, exception);
        }

        /// <summary>
        /// 记录Debug级别日志
        /// </summary>
        public static void DebugFormat(this ILogger logger, string format, params object[] args) {
            logger.Log(LogLevel.Debug, format, args);
        }

        /// <summary>
        /// 记录Info级别日志
        /// </summary>
        public static void Info(this ILogger logger, object message) {
            logger.Log(LogLevel.Information, message);
        }

        /// <summary>
        /// 记录Info级别日志
        /// </summary>
        public static void Info(this ILogger logger, object message, Exception exception) {
            logger.Log(LogLevel.Information, message, exception);
        }

        /// <summary>
        /// 记录Info级别日志
        /// </summary>
        public static void InfoFormat(this ILogger logger, string format, params object[] args) {
            logger.Log(LogLevel.Information, format, args);
        }

        /// <summary>
        /// 记录Error级别日志
        /// </summary>
        public static void Error(this ILogger logger, object message) {
            logger.Log(LogLevel.Error, message);
        }

        /// <summary>
        /// 记录Error级别日志
        /// </summary>
        public static void Error(this ILogger logger, object message, Exception exception) {
            logger.Log(LogLevel.Error, message, exception);
        }

        /// <summary>
        /// 记录Error级别日志
        /// </summary>
        public static void ErrorFormat(this ILogger logger, string format, params object[] args) {
            logger.Log(LogLevel.Error, format, args);
        }

        /// <summary>
        /// 记录Warn级别日志
        /// </summary>
        public static void Warn(this ILogger logger, object message) {
            logger.Log(LogLevel.Warning, message);
        }

        /// <summary>
        /// 记录Warn级别日志
        /// </summary>
        public static void Warn(this ILogger logger, object message, Exception exception) {
            logger.Log(LogLevel.Warning, message, exception);
        }

        /// <summary>
        /// 记录Warn级别日志
        /// </summary>
        public static void WarnFormat(this ILogger logger, string format, params object[] args) {
            logger.Log(LogLevel.Warning, format, args);
        }

        /// <summary>
        /// 记录Fatal级别日志
        /// </summary>
        public static void Fatal(this ILogger logger, object message) {
            logger.Log(LogLevel.Fatal, message);
        }

        /// <summary>
        /// 记录Fatal级别日志
        /// </summary>
        public static void Fatal(this ILogger logger, object message, Exception exception) {
            logger.Log(LogLevel.Fatal, message, exception);
        }

        /// <summary>
        /// 记录Fatal级别日志
        /// </summary>
        public static void FatalFormat(this ILogger logger, string format, params object[] args) {
            logger.Log(LogLevel.Fatal, format, args);
        }
    }
}