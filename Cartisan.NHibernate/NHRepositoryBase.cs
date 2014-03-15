//using System.Collections.Generic;
//using NHibernate;
//using YouQiu.Framework.Domain;
//using YouQiu.Framework.UnitOfWork;
//
//namespace YouQiu.Framework.Repository.NHibernate {
//    public abstract class NHRepositoryBase<T, TId> : RepositoryBase<T, TId> where T : IAggregateRoot {
//        protected NHRepositoryBase(IUnitOfWork unitOfWork)
//            : base(unitOfWork) {
//        }
//
//        public override void PersistCreationOf(IAggregateRoot entity) {
//            SessionProvider.GetCurrentSession().Save(entity);
//        }
//
//        public override void PersistUpdateOf(IAggregateRoot entity) {
//            SessionProvider.GetCurrentSession().Save(entity);
//        }
//
//        public override void PersistDeletionOf(IAggregateRoot entity) {
//            SessionProvider.GetCurrentSession().Delete(entity);
//        }
//
//        public override T Load(TId id) {
//            return SessionProvider.GetCurrentSession().Load<T>(id);
//        }
//
//        public override T FindBy(TId id) {
//            return SessionProvider.GetCurrentSession().Get<T>(id);
//        }
//
//        public override IEnumerable<T> FindAll() {
//            return SessionProvider.GetCurrentSession().CreateCriteria(typeof(T)).List<T>();
//        }
//
//        //public override IEnumerable<T> FindBy(Query query) {
//        //    ICriteria criteriaQuery = SessionProvider.GetCurrentSession().CreateCriteria(typeof(T));
//        //    AppendCriteria(criteriaQuery);
//
//        //    query.TranslateIntoNHQuery<T>(criteriaQuery);
//        //    return criteriaQuery.List<T>();
//        //}
//
//        //public override IEnumerable<T> FindBy(Query query, int index, int count) {
//        //    ICriteria criteriaQuery = SessionProvider.GetCurrentSession().CreateCriteria(typeof(T));
//        //    AppendCriteria(criteriaQuery);
//
//        //    query.TranslateIntoNHQuery<T>(criteriaQuery);
//
//        //    return criteriaQuery.SetFetchSize(count).List<T>();
//        //}
//
//        public virtual void AppendCriteria(ICriteria criteria) {
//            
//        }
//
//        //public override IEnumerable<T> FindAll(int index, int count) {
//        //    throw new NotImplementedException();
//        //}
//
//        //public override IEnumerable<T> QueryWith(ISpecification<T> specification) {
//        //    throw new NotImplementedException();
//        //}
//
//        //public override IEnumerable<T> QueryWith(ISpecification<T> specification, int index, int count) {
//        //    throw new NotImplementedException();
//        //}
//    }
//}