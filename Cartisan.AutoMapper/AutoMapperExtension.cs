using System.Linq;
using AutoMapper;

namespace Cartisan.AutoMapper {
    public static class AutoMapperExtension {
        public static T MapTo<T>(this object obj) {
            return Mapper.Map<T>(obj);
        }

        public static T MapTo<T>(this IQueryable queryable) {
            return Mapper.Map<T>(queryable);
        }
    }
}