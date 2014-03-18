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


//public class NHUnitOfWork: IUnitOfWork {
//    protected IList<IAggregateRoot> _addedEntities;
//    protected IList<IAggregateRoot> _changedEntities;
//    protected IList<IAggregateRoot> _deletedEntities;
//
//    public NHUnitOfWork() {
//        _addedEntities = new List<IAggregateRoot>();
//        _changedEntities = new List<IAggregateRoot>();
//        _deletedEntities = new List<IAggregateRoot>();
//    }
//
//    public void RegisterAmended(IAggregateRoot entity) {
//        if (_deletedEntities.Contains(entity) || _addedEntities.Contains(entity)) {
//            return;
//        }
//        if (_changedEntities.Contains(entity)) {
//            return;
//        }
//        this._changedEntities.Add(entity);
//    }
//
//    public void RegisterNew(IAggregateRoot entity) {
//        if (_deletedEntities.Contains(entity)) {
//            return;
//        }
//        if (_changedEntities.Contains(entity)) {
//            _changedEntities.Remove(entity);
//        }
//        if (_addedEntities.Contains(entity)) {
//            return;
//        }
//        _addedEntities.Add(entity);
//    }
//
//    public void RegisterRemoved(IAggregateRoot entity) {
//        if (_addedEntities.Contains(entity)) {
//            _addedEntities.Remove(entity);
//        }
//        if (_changedEntities.Contains(entity)) {
//            _changedEntities.Remove(entity);
//        }
//        if (_deletedEntities.Contains(entity)) {
//            return;
//        }
//        _deletedEntities.Add(entity);
//    }
//
//    public virtual void Commit() {
//        //            using(TransactionScope scope = new TransactionScope()) {
//        //                PersistExecuteRepositoryOperator();
//        //                scope.Complete();
//        //                ClearRepositories();
//        //            }
//
//        using (ITransaction transaction = SessionProvider.GetCurrentSession().BeginTransaction()) {
//            try {
//                PersistExecuteRepositoryOperator();
//                transaction.Commit();
//                ClearRepositories();
//            }
//            catch (Exception) {
//                transaction.Rollback();
//                throw;
//            }
//        }
//    }
//
//    protected void PersistExecuteRepositoryOperator() {
//        foreach (var entity in _addedEntities) {
//            SessionProvider.GetCurrentSession().Save(entity);
//        }
//
//        foreach (var entity in _changedEntities) {
//            SessionProvider.GetCurrentSession().Save(entity);
//        }
//
//        foreach (var entity in _deletedEntities) {
//            SessionProvider.GetCurrentSession().Delete(entity);
//        }
//    }
//
//    protected void ClearRepositories() {
//        _addedEntities.Clear();
//        _changedEntities.Clear();
//        _deletedEntities.Clear();
//    }
//}