using System.Linq;
using System.Reflection;
using Autofac;
using Cartisan.Infrastructure;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Autofac {
    public static class AutofacExtension {
        public static void RegisterIDependency(this ContainerBuilder builder, params Assembly[] assemblies) {
            assemblies.SelectMany(assembly => assembly.GetTypes())
                .ForEach(type => {
                    if (typeof(IDependency).IsAssignableFrom(type)) {
                        builder.RegisterType(type);
                    }
                });
        }
    }
}