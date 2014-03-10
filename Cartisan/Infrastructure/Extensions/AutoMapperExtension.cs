//using System;
//using System.Linq.Expressions;
//
//namespace Cartisan.Infrastructure.Extensions {
//
//    public class AutoMapForMemberOption<TSource, TDestination>
//        where TSource: class
//        where TDestination: class {
//        public AutoMapForMemberOption() {
//
//        }
//        public AutoMapForMemberOption(Expression<Func<TDestination, object>> destinationMember,
//            Action<IMemberConfigurationExpression<TSource>> memberOptions) {
//            DestinationMember = destinationMember;
//            MemberOptions = memberOptions;
//        }
//
//        public Expression<Func<TDestination, object>> DestinationMember { get; set; }
//        public Action<IMemberConfigurationExpression<TSource>> MemberOptions { get; set; }
//    }
//
//    public static class AutoMapperExtension {
//        public static TDestination ToMapObject<TDestination>
//         (this object src)
//           where TDestination: class {
//            var mapper = Mapper.CreateMap(src.GetType(), typeof(TDestination));
//            var dest = Mapper.Map(src, src.GetType(), typeof(TDestination)) as TDestination;
//            return dest;
//        }
//
//        public static TDestination ToMapObject<TSource, TDestination>
//           (this TSource src,
//           params AutoMapForMemberOption<TSource, TDestination>[] memberOptions)
//            where TSource: class
//            where TDestination: class {
//            return ToMapObject(src, null, null, memberOptions);
//        }
//
//        public static TDestination ToMapObject<TSource, TDestination>
//           (this TSource src, TDestination dest,
//           params AutoMapForMemberOption<TSource, TDestination>[] memberOptions)
//            where TSource: class
//            where TDestination: class {
//            return ToMapObject(src, dest, null, memberOptions);
//        }
//
//        public static TDestination ToMapObject<TSource, TDestination>
//          (this TSource src, TDestination dest,
//          params Expression<Func<TDestination, object>>[] ignoreMembers)
//            where TSource: class
//            where TDestination: class {
//            return ToMapObject(src, dest, ignoreMembers, null);
//        }
//
//        public static TDestination ToMapObject<TSource, TDestination>
//            (this TSource src, TDestination dest,
//            IEnumerable<Expression<Func<TDestination, object>>> ignoreMembers,
//            AutoMapForMemberOption<TSource, TDestination>[] memberOptions)
//            where TSource: class
//            where TDestination: class {
//            var mapper = Mapper.CreateMap<TSource, TDestination>();
//
//            if (ignoreMembers != null && ignoreMembers.Count() > 0) {
//                ignoreMembers.ForEach(ignore => {
//                    mapper.ForMember(ignore, opt => opt.Ignore());
//                });
//            }
//
//            if (memberOptions != null && memberOptions.Length > 0) {
//                memberOptions.ForEach(mo => {
//                    mapper.ForMember(mo.DestinationMember, mo.MemberOptions);
//                });
//            }
//            if (dest == null) {
//                dest = Mapper.Map<TSource, TDestination>(src) as TDestination;
//            }
//            else {
//                dest = Mapper.Map(src, dest) as TDestination;
//            }
//            return dest;
//        }
//
//        //public static object ToMapObject(this object src, object dest)
//        //{
//        //    Mapper.CreateMap(src.GetType(), dest.GetType());
//        //    if (dest == null)
//        //    {
//        //        dest = Mapper.Map(src, src.GetType(), dest.GetType());
//        //    }
//        //    else
//        //    {
//        //        dest = Mapper.Map(src, dest, src.GetType(), dest.GetType());
//        //    }
//        //    return dest;
//        //}
//
//        public static TDestination CreateObject<TSource, TDestination>(TSource obj) {
//            Mapper.CreateMap<TSource, TDestination>();
//            var dest = Mapper.Map<TSource, TDestination>(obj);
//            return dest;
//        }
//
//        public static void UpdateObject<TSource, TDestination>(TSource obj, TDestination destObj) {
//            Mapper.CreateMap<TSource, TDestination>();
//            Mapper.Map<TSource, TDestination>(obj, destObj);
//        }
//
//        public static void UpdateObject(dynamic srcObj, dynamic destObj) {
//            Mapper.CreateMap(srcObj.GetType(), destObj.GetType());
//            Mapper.Map(srcObj, destObj, srcObj.GetType(), destObj.GetType());
//        }
//    }
//}