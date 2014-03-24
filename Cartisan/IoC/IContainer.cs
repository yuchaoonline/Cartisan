using System;

namespace Cartisan.IoC {
    public interface IContainer {
        TService Resolve<TService>();
        object Resolve(Type type);
        void RegisterType(Type type);
    }
}