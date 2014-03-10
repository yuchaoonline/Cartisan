using Cartisan.Domain;

namespace Cartisan.EntityFramework.Extensions {
    public static class QueryableCollectionInitializer {
        public static void InitializeQueryableCollections(this ContextBase context, object entity) {
            Entity dbEntity = entity as Entity;
            if(dbEntity!=null) {
                dbEntity.DomainContext = context;
            }
        }
    }
}