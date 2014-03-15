namespace Cartisan.Web.Mvc.Extensions {
    public static class ConfigurationExtension {
//        static IUnityContainer _CurrentContainer = IoCFactory.Instance.CurrentContainer;
//
//        public static Configuration MvcIgnoreResouceRoute(this Configuration configuration, RouteCollection routes) {
//            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
//            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
//            routes.IgnoreRoute("{*alljs}", new { alljs = @".*\.js(/.*)?" });
//            routes.IgnoreRoute("{*allcss}", new { allcss = @".*\.css(/.*)?" });
//            routes.IgnoreRoute("{*alljpg}", new { alljpg = @".*\.jpg(/.*)?" });
//            routes.IgnoreRoute("{*allgif}", new { allgif = @".*\.gif(/.*)?" });
//            routes.IgnoreRoute("{*allpng}", new { allpng = @".*\.png(/.*)?" });
//            routes.IgnoreRoute("{*allswf}", new { allswf = @".*\.swf(/.*)?" });
//            routes.IgnoreRoute("{*allcur}", new { allcur = @".*\.cur(/.*)?" });
//            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
//
//            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
//            return configuration;
//        }
//
//        public static Configuration RegisterDisposeModule(this Configuration configuration) {
//            DynamicModuleUtility.RegisterModule(typeof(DisposeObjectHttpModule));
//            return configuration;
//        }
//
//        //public static Configuration RegisterIoCPageHandler(this Configuration configuration)
//        //{
//        //    using (ServerManager manager = new ServerManager())
//        //    {
//        //        var webConfig = manager.GetApplicationHostConfiguration();
//
//        //        ConfigurationSection handlersSection =
//        //            webConfig.GetSection("system.webServer/handlers");
//
//        //        ConfigurationElementCollection handlersCollection = handlersSection.GetCollection();
//
//        //        ConfigurationElement removeElement = handlersCollection.CreateElement("remove");
//        //        removeElement.SetAttributeValue("name", "IoCPageHandlerFactory");
//
//        //        ConfigurationElement handlerElement = handlersCollection.CreateElement("add");
//
//        //        handlerElement.SetAttributeValue("name", "IoCPageHandlerFactory");
//        //        handlerElement.SetAttributeValue("path", "*.aspx");
//        //        handlerElement.SetAttributeValue("verb", "*");
//        //        handlerElement.SetAttributeValue("type", "IFramework.Infrastructure.Mvc.IoCPageHandlerFactory");
//
//        //        handlersCollection.Add(removeElement);
//        //        handlersCollection.Add(handlerElement);
//
//        //        manager.CommitChanges();
//        //    }
//        //    return configuration;
//        //}
//
//        public static Configuration RegisterMvc(this Configuration configuration) {
//            RegisterControllers();
//
//            //Register new model binders
//            RegisterModelBinders();
//
//            //register factories
//            RegisterFactories();
//            return configuration;
//        }
//
//
//
//        static void RegisterFactories() {
//            //Create a custom controller factory to resolve controllers with IoC container
//            IControllerFactory factory = new IoCControllerFactory(_CurrentContainer);
//            //Set new controller factory in ASP.MVC Controller builder
//            ControllerBuilder.Current.SetControllerFactory(factory);
//
//            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(_CurrentContainer);
//        }
//
//        static void RegisterModelBinders() {
//            //Register a new model binder for customers. This model binder enables the deserialization
//            //of a given customer in edit scenarios.
//            //ModelBinders.Binders.Add(typeof(Customer), new SelfTrackingEntityModelBinder<Customer>());
//
//
//            ////Register a new model binder for customer's picture. This model binder binds the posted
//            ////image to a the byte array field in the CustomerPicture class.
//            //ModelBinders.Binders.Add(typeof(CustomerPicture), new CustomerPictureModelBinder());
//        }
//
//        static void RegisterControllers() {
//            //In this implementations only controllers in same assembly are registered in IoC container. If you
//            // have controllers in a different assembly check this code.
//
//            //Recover excuting assembly.
//            var assemblies = GetRegisterAssemblies();
//            foreach (var assembly in assemblies) {
//                IEnumerable<Type> controllers = assembly.GetExportedTypes()
//                                                    .Where(x => typeof(IController).IsAssignableFrom(x)
//                                                            || typeof(IHttpController).IsAssignableFrom(x));
//
//                //Register all controllers types
//                foreach (Type item in controllers)
//                    _CurrentContainer.RegisterType(item);
//            }
//            //Recover all controller types in this assembly.
//
//        }
//
//        static List<Assembly> GetRegisterAssemblies() {
//            List<Assembly> assemblies = new List<Assembly>();
//
//            var mvcControllerAssemblies = ConfigurationReader.Instance
//                                                             .GetConfigurationSection<MvcConfigurationSection>()
//                                                             .MvcControllers;
//            if (mvcControllerAssemblies != null) {
//                foreach (MvcControllerElement mvcControllerAssembly in mvcControllerAssemblies) {
//                    try {
//                        assemblies.Add(Assembly.Load(mvcControllerAssembly.Assembly));
//                    }
//                    catch (System.Exception) {
//                    }
//                }
//            }
//            return assemblies;
//        }
    }
}