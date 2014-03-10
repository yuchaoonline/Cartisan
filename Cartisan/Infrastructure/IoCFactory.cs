using System;

namespace Cartisan.Infrastructure {
    public static class IoCFactory {
        public static IContainer Container { get; set; }

        public static TService Resolve<TService>() {
            return Container.Resolve<TService>();
        }
        
        public static object Resolve(Type type) {
            return Container.Resolve(type);
        }
    }
}