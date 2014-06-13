using System;
using System.Collections;
using System.Collections.Generic;

namespace Cartisan.IoC {
    // TODO: 需要丰富解析方式，如根据命名、Key、参数等
    public static class ServiceLocator {
        private static IResolver Resolver { get; set; }

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

        public static void RegisterResolver(IResolver resolver) {
            Resolver = resolver;
        }
    }
}