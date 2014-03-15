//using System.Collections;
//using System.Threading;
//using NHibernate;
//
//namespace YouQiu.Framework.Repository.NHibernate.SessionStorage {
//    /// <summary>
//    /// 非Web应用下，使用线程保存NHibernate的Session的容器
//    /// </summary>
//    public class ThreadSessionStorageContainer : ISessionStorageContainer {
//        private static readonly Hashtable _nhSessions = new Hashtable();
//
//        public ISession GetCurrentSession() {
//            ISession session = null;
//
//            if(_nhSessions.Contains(GetThreadName())) {
//                session = (ISession)_nhSessions[GetThreadName()];
//            }
//
//            return session;
//        }
//
//        public void Store(ISession session) {
//            if(_nhSessions.Contains(GetThreadName())) {
//                _nhSessions[GetThreadName()] = session;
//            }
//            else {
//                _nhSessions.Add(GetThreadName(), session);
//            }
//        }
//
//        private static string GetThreadName() {
//            if(Thread.CurrentThread.Name==null) {
//                Thread.CurrentThread.Name = "MainThread";
//            }
//            return Thread.CurrentThread.Name;
//        }
//    }
//}