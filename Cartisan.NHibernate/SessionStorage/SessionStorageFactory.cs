//using System.Web;
//
//namespace YouQiu.Framework.Repository.NHibernate.SessionStorage {
//    /// <summary>
//    /// SessionÈÝÆ÷¹¤³§
//    /// </summary>
//    public static class SessionStorageFactory {
//        private static ISessionStorageContainer _nhSessionStorageContainer;
//
//        public static ISessionStorageContainer GetStorageContainer() {
//            if(_nhSessionStorageContainer==null) {
//                if(HttpContext.Current!=null) {
//                    _nhSessionStorageContainer = new HttpSessionContainer();
//                }
//                else {
//                    _nhSessionStorageContainer = new ThreadSessionStorageContainer();
//                }
//            }
//            return _nhSessionStorageContainer;
//        }
//    }
//}