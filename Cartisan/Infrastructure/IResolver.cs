using System;
using System.Collections;
using System.Collections.Generic;

namespace Cartisan.Infrastructure {
    public interface IResolver {
        object GetService(Type serviceType);
        TService GetService<TService>();

        IEnumerable GetServices(Type serviceType);
        IEnumerable<TService> GetServices<TService>(); 
    }
}