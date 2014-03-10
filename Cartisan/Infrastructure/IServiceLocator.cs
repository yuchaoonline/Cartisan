using System;

namespace Cartisan.Infrastructure {
    public interface IServiceLocator {
        object GetService(Type serviceType);
        TService GetService<TService>();
    }
}