//using System.Web;
//using NHibernate;
//
//namespace YouQiu.Framework.Repository.NHibernate.SessionStorage {
//    /// <summary>
//    /// Web应用下保存NHibernate的Session的容器
//    /// </summary>
//    public class HttpSessionContainer : ISessionStorageContainer {
//        private string _sessionKey = "NHSession";
//
//        public ISession GetCurrentSession() {
//            ISession session = null;
//
//            if(HttpContext.Current.Items.Contains(_sessionKey)) {
//                session = (ISession)HttpContext.Current.Items[_sessionKey];
//            }
//
//            return session;
//        }
//
//        public void Store(ISession session) {
//            if(HttpContext.Current.Items.Contains(_sessionKey)) {
//                HttpContext.Current.Items[_sessionKey] = session;
//            }
//            else {
//                HttpContext.Current.Items.Add(_sessionKey, session);
//            }
//        }
//    }
//}