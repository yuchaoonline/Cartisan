using System;

namespace Cartisan.Infrastructure.Log {
    public interface ILoggerFactory {
        ILogger Create(string name);
        ILogger Create(Type type);
    }
}