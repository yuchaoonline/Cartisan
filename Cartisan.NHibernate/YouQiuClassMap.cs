//using System;
//using FluentNHibernate.Mapping;
//using YouQiu.Framework.Domain;
//
//namespace YouQiu.Framework.Repository.NHibernate {
//    public class YouQiuClassMap<T> : ClassMap<T> where T : EntityBase<int> {
//        public YouQiuClassMap() {
//            this.Id(entity => entity.Id);
//            this.Version(entity => entity.Version);
//        }
//    }
//}