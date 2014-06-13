using System;

namespace Cartisan.Log {
    /// <summary>
    /// 日志
    /// </summary>
    public interface ILogger {
        /// <summary>
        /// 检查level级别的日志是否启用
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        bool IsEnabled(LogLevel level);

        /// <summary>
        /// 记录level级别的日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">需要记录的内容</param>
        void Log(LogLevel level, object message);

        /// <summary>
        /// 记录level级别的日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">需要记录的内容</param>
        /// <param name="exception">异常</param>
        void Log(LogLevel level, object message, Exception exception);

        /// <summary>
        /// 记录level级别的日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="format">需记录的内容格式</param>
        /// <param name="args">替换format占位符的参数</param>
        void Log(LogLevel level, string format, params object[] args);
    }
}