using System;

namespace Cartisan.Log {
    /// <summary>
    /// 日志工厂
    /// </summary>
    public interface ILoggerFactory {
        ILogger Create(string name);
        ILogger Create(Type type);
    }
}