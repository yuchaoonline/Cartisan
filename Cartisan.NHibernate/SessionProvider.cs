//using System;
//using System.Configuration;
//using System.Linq;
//using NHibernate;
//using nhCfg = NHibernate.Cfg;
//using YouQiu.Framework.Repository.NHibernate.SessionStorage;
//
//namespace YouQiu.Framework.Repository.NHibernate {
//    public class SessionProvider {
//        private static ISessionFactory _sessionFactory;
//
//        private static void Init() {
////            _sessionFactory = Fluently.Configure()
////                .Database(
////                    MsSqlConfiguration.MsSql2008.ConnectionString(
////                        ConfigurationManager.ConnectionStrings["YouQiuConnectionString"].ConnectionString))
////                .BuildConfiguration().BuildSessionFactory();
//            var config = new nhCfg.Configuration();
//            ConfigurationManager.AppSettings["NHibernateRepositoryAssembly"].Split(",".ToCharArray(),
//                StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(assembly => config.AddAssembly(assembly));
//            //config.AddAssembly(ConfigurationManager.AppSettings["NHibernateRepositoryAssembly"]);
//
//            //log4net.Config.XmlConfigurator.Configure();
//
//            config.Configure();
//
//            _sessionFactory = config.BuildSessionFactory();
//        }
//
//        private static ISessionFactory GetSessionFactory() {
//            if (_sessionFactory == null) {
//                Init();
//            }
//            return _sessionFactory;
//        }
//
//        private static ISession GetNewSession() {
//            return GetSessionFactory().OpenSession();
//        }
//
//        public static ISession GetCurrentSession() {
//            ISessionStorageContainer sessionStorageContainer = SessionStorageFactory.GetStorageContainer();
//
//            ISession currentSession = sessionStorageContainer.GetCurrentSession();
//            if (currentSession == null || !currentSession.IsOpen) {
//                currentSession = GetNewSession();
//                sessionStorageContainer.Store(currentSession);
//            }
//            return currentSession;
//        }
//    }
//}