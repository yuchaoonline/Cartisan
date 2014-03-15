//using System;
//using NHibernate;
//using YouQiu.Framework.UnitOfWork;
//
//namespace YouQiu.Framework.Repository.NHibernate {
//    public class NHUnitOfWork : UnitOfWorkBase {
//        public override void Commit() {
//            using(ITransaction transaction = SessionProvider.GetCurrentSession().BeginTransaction()) {
//                try {
//                    PersistExecuteRepositoryOperator();
//                    transaction.Commit();
//                    ClearRepositories();
//                }
//                catch(Exception) {
//                    transaction.Rollback();
//                    throw;
//                }
//            }
//        }
//    }
//}