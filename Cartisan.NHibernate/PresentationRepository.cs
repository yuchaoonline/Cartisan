//using System;
//using System.Collections.Generic;
//using System.Linq;
//using NHibernate;
//using NHibernate.Criterion;
//
////using NHibernate.Linq;
//
//namespace YouQiu.Framework.Repository.NHibernate {
////    public class PresentationRepository : IPresentationRepository {
////        public IEnumerable<T> FindByType<T>() {
////            using(var session = SessionProvider.GetCurrentSession()) {
////                ICriteria criteriaQuery = session.CreateCriteria(typeof(T));
////                return criteriaQuery.List<T>();
////            }
////        }
////
////        public IEnumerable<T> FindByExample<T>(object propertiesAndValues) {
////            var dictionary = GetPropertyInfomation(propertiesAndValues);
////
////            using(var session = SessionProvider.GetCurrentSession()) {
////                ICriteria criteriaQuery = session.CreateCriteria(typeof(T));
////                foreach(var pair in dictionary) {
////                    ICriterion criterion = Expression.Eq(pair.Key, pair.Value);
////                    criteriaQuery.Add(criterion);
////                }
////                return criteriaQuery.List<T>();
////            }
////        }
////
////        public IEnumerable<T> FindBySpecification<T>(ISpecification<T> specification) {
////            throw new NotImplementedException();
////        }
////
////        //public IEnumerable<T> FindBySpecification<T>(ISpecification<T> specification) {
////        //    using(var session = SessionProvider.GetCurrentSession()) {
////        //        return session.Query<T>().Where(specification.IsSatisfied()).ToList();
////        //    }
////        //}
////
////        public T FindFirstByExample<T>(object propertiesAndValues) {
////            var dictionary = GetPropertyInfomation(propertiesAndValues);
////
////            using (var session = SessionProvider.GetCurrentSession()) {
////                ICriteria criteriaQuery = session.CreateCriteria(typeof(T));
////                foreach (var pair in dictionary) {
////                    ICriterion criterion = Expression.Eq(pair.Key, pair.Value);
////                    criteriaQuery.Add(criterion);
////                }
////                return criteriaQuery.List<T>().SingleOrDefault();
////            }
////        }
////
////        public T FindfirstBySpec<T>(ISpecification<T> specification) {
////            throw new NotImplementedException();
////        }
////
////        //public T FindfirstBySpec<T>(ISpecification<T> specification) {
////        //    using (var session = SessionProvider.GetCurrentSession()) {
////        //        return session.Query<T>().Where(specification.IsSatisfied()).FirstOrDefault();
////        //    }
////        //}
////
////        private Dictionary<string, object> GetPropertyInfomation(object  propertiesAndValues) {
////            var exampleData = new Dictionary<string, object>();
////            propertiesAndValues.GetType().GetProperties().ToList().ForEach(
////                propertyInfo =>
////                    exampleData.Add(propertyInfo.Name, propertyInfo.GetValue(propertiesAndValues, new object[] {})));
////
////            return exampleData;
////        }
////    }
//}