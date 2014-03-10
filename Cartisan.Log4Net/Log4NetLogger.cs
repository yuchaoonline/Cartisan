using System;
using Cartisan.Infrastructure.Log;
using log4net;

namespace Cartisan.Log4Net {
    public class Log4NetLogger: ILogger {
        private readonly ILog _log;

        public Log4NetLogger(ILog log) {
            this._log = log;
        }

        public bool IsDebugEnabled {
            get { return _log.IsDebugEnabled; }
        }

        public void Debug(object message) {
            if(IsDebugEnabled) {
                _log.Debug(message);
            }
        }

        public void Debug(object message, Exception exception) {
            if (IsDebugEnabled) {
                _log.Debug(message, exception);
            }
        }

        public void DebugFormat(string format, params object[] args) {
            if (IsDebugEnabled) {
                _log.DebugFormat(format, args);
            }
        }

        public void Info(object message) {
            _log.Info(message);
        }

        public void Info(object message, Exception exception) {
            _log.Info(message, exception);
        }

        public void InfoFormat(string format, params object[] args) {
            _log.InfoFormat(format, args);
        }

        public void Error(object message) {
            _log.Error(message);
        }

        public void Error(object message, Exception exception) {
            _log.Error(message, exception);
        }

        public void ErrorFormat(string format, params object[] args) {
            _log.ErrorFormat(format, args);
        }

        public void Warn(object message) {
            _log.Warn(message);
        }

        public void Warn(object message, Exception exception) {
            _log.Warn(message, exception);
        }

        public void WarnFormat(string format, params object[] args) {
            _log.WarnFormat(format, args);
        }

        public void Fatal(object message) {
            _log.Fatal(message);
        }

        public void Fatal(object message, Exception exception) {
            _log.Fatal(message, exception);
        }

        public void FatalFormat(string format, params object[] args) {
            _log.FatalFormat(format, args);
        }
    }
}