//using System;
//using System.Collections.Generic;
//using Easier.Querying;
//using NHibernate;
//using NHibernate.Criterion;
//
//namespace Easier.Repository.NHibernate {
//    public static class QueryTranslator {
//        public static ICriteria TranslateIntoNHQuery<T>(this Query query, ICriteria criteria) {
//            BuildQueryFrom(query, criteria);
//
//            if(query.OrderByProperty!=null) {
//                criteria.AddOrder(new Order(query.OrderByProperty.PropertyName, !query.OrderByProperty.Desc));
//            }
//
//            return criteria;
//        }
//
//        public static void BuildQueryFrom(Query query, ICriteria criteria) {
//            var criterions = new List<ICriterion>();
//
//            if(query.Criteria!=null) {
//                foreach(var c in query.Criteria) {
//                    ICriterion criterion;
//                    switch(c.CriteriaOperator) {
//                        case CriteriaOperator.Equal:
//                            criterion = Expression.Eq(c.PropertyName, c.Value);
//                            break;
//                        case CriteriaOperator.LesserThanOrEqual:
//                            criterion = Expression.Le(c.PropertyName, c.Value);
//                            break;
//                        default:
//                            throw new ApplicationException("No operator defined");
//                    }
//                    criterions.Add(criterion);
//                }
//            }
//
//            if(query.QueryOperator == QueryOperator.And) {
//                var andSubQuery = Expression.Conjunction();
//                foreach(var criterion in criterions) {
//                    andSubQuery.Add(criterion);
//                }
//                criteria.Add(andSubQuery);
//            }
//            else {
//                var orSubQuery = Expression.Disjunction();
//                foreach(var criterion in criterions) {
//                    orSubQuery.Add(criterion);
//                }
//                criterions.Add(orSubQuery);
//            }
//
//            foreach(var subQuery in query.SubQueries) {
//                BuildQueryFrom(subQuery, criteria);
//            }
//        }
//    }
//}