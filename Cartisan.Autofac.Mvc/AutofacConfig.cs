using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace Cartisan.Autofac.Mvc {
    public class AutofacConfig {
        private static string assemblySkipLoadingPattern =
            "^System|^mscorlib|^Microsoft|^CppCodeProvider|^VJSharpCodeProvider|^WebDev|^Castle|^Iesi|^log4net|^NHibernate|^nunit|^TestDriven|^MbUnit|^Rhino|^QuickGraph|^TestFu|^Telerik|^ComponentArt|^MvcContrib|^AjaxControlToolkit|^Antlr3|^Remotion|^Recaptcha";
        private static string assemblyRestrictToLoadingPattern = ".*";

        public static void Initialize() {
            ContainerBuilder builder = new ContainerBuilder();

            AppDomain.CurrentDomain.GetAssemblies().Select(assembly => Matches(assembly.FullName));

            Assembly[] assemblies =
                AppDomain.CurrentDomain.GetAssemblies().Where(assembly => Matches(assembly.FullName)).ToArray();
//            builder.RegisterIDependency(assemblies);
//
//            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
//
//            builder.RegisterAssemblyModules(assemblies);
//
//            builder.RegisterControllers(assemblies);
//
//            IContainer container = builder.Build();
//            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
//            ServiceLocator.Resolver = new MvcResolver();
        }

        private static bool Matches(string assemblyFullName) {
            return !Matches(assemblyFullName, assemblySkipLoadingPattern)
                   && Matches(assemblyFullName, assemblyRestrictToLoadingPattern);
        }

        private static bool Matches(string assemblyFullName, string pattern) {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        } 
    }
}