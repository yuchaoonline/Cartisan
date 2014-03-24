using System;

namespace Cartisan.IoC {
    public interface IServiceLocator {
        object GetService(Type serviceType);
        TService GetService<TService>();
    }
}