using System;

namespace Cartisan.Log {
    /// <summary>
    /// 日志工厂适配器
    /// </summary>
    public interface ILoggerFactoryAdapter {
        ILogger GetLogger(string name);
        ILogger GetLogger(Type type);
    }
}