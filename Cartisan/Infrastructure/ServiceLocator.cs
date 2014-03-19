using System;
using System.Collections;
using System.Collections.Generic;

namespace Cartisan.Infrastructure {
    public static class ServiceLocator {
        public static IResolver Resolver { get; set; }

        public static object GetService(Type serviceType) {
            return Resolver.GetService(serviceType);
        }

        public static TService GetService<TService>() {
            return Resolver.GetService<TService>();
        }

        public static IEnumerable GetServices(Type serviceType) {
            return Resolver.GetServices(serviceType);
        }

        public static IEnumerable<TService> GetServices<TService>() {
            return Resolver.GetServices<TService>();
        }
    }
}