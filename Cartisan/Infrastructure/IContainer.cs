using System;

namespace Cartisan.Infrastructure {
    public interface IContainer {
        TService Resolve<TService>();
        object Resolve(Type type);
        void RegisterType(Type type);
    }
}