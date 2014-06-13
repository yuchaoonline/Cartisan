using System;
using Cartisan.Log;
using log4net;

namespace Cartisan.Log4Net {
    public class Log4NetLogger: ILogger {
        private readonly ILog _log;

        public Log4NetLogger(ILog log) {
            this._log = log;
        }

        public bool IsEnabled(LogLevel level) {
            switch(level) {
                case LogLevel.Debug:
                    return this._log.IsDebugEnabled;
                case LogLevel.Information:
                    return this._log.IsInfoEnabled;
                case LogLevel.Warning:
                    return this._log.IsWarnEnabled;
                case LogLevel.Error:
                    return this._log.IsErrorEnabled;
                case LogLevel.Fatal:
                    return this._log.IsFatalEnabled;
                default:
                    return false;
            }
        }

        public void Log(LogLevel level, object message) {
            if(!this.IsEnabled(level)) {
                return;
            }
            switch (level) {
                case LogLevel.Debug:
                    this._log.Debug(message);
                    break;
                case LogLevel.Information:
                    this._log.Info(message);
                    break;
                case LogLevel.Warning:
                    this._log.Warn(message);
                    break;
                case LogLevel.Error:
                    this._log.Error(message);
                    break;
                case LogLevel.Fatal:
                    this._log.Fatal(message);
                    break;
            }
        }

        public void Log(LogLevel level, object message, Exception exception) {
            if (!this.IsEnabled(level)) {
                return;
            }
            switch (level) {
                case LogLevel.Debug:
                    this._log.Debug(message, exception);
                    break;
                case LogLevel.Information:
                    this._log.Info(message, exception);
                    break;
                case LogLevel.Warning:
                    this._log.Warn(message, exception);
                    break;
                case LogLevel.Error:
                    this._log.Error(message, exception);
                    break;
                case LogLevel.Fatal:
                    this._log.Fatal(message, exception);
                    break;
            }
        }

        public void Log(LogLevel level, string format, params object[] args) {
            if (!this.IsEnabled(level)) {
                return;
            }
            switch (level) {
                case LogLevel.Debug:
                    this._log.DebugFormat(format, args);
                    break;
                case LogLevel.Information:
                    this._log.InfoFormat(format, args);
                    break;
                case LogLevel.Warning:
                    this._log.WarnFormat(format, args);
                    break;
                case LogLevel.Error:
                    this._log.ErrorFormat(format, args);
                    break;
                case LogLevel.Fatal:
                    this._log.FatalFormat(format, args);
                    break;
            }
        }
    }
}