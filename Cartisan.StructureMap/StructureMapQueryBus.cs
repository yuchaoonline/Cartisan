using Cartisan.QueryProcessor.Dispatcher;
using Cartisan.QueryProcessor.Query;
using StructureMap;

namespace Cartisan.StructureMap {
    public class StructureMapQueryBus: IQueryBus {
        public ISingleResult<TResult> QuerySingle<TQuery, TResult>(TQuery query) where TQuery: IQuery {
            var handler = ObjectFactory.GetInstance(typeof(IQuerySingleHandler<TQuery, TResult>)) as
                IQuerySingleHandler<TQuery, TResult>;

            if (handler == null) {
                throw new QueryHandlerNotFoundException(typeof(TQuery));
            }

            return handler.Execute(query);
        }

        public IListResult<TResult> QueryList<TQuery, TResult>(TQuery query) where TQuery: IQuery {
            var handler = ObjectFactory.GetInstance(typeof(IQueryListHandler<TQuery, TResult>)) as
                IQueryListHandler<TQuery, TResult>;

            if (handler == null) {
                throw new QueryHandlerNotFoundException(typeof(TQuery));
            }

            return handler.Execute(query);
        }

        public IPageResult<TResult> QueryPager<TQuery, TResult>(TQuery query, PageOption pageOption) where TQuery: IQuery {
            var handler = ObjectFactory.GetInstance(typeof(IQueryPageHandler<TQuery, TResult>)) as
                IQueryPageHandler<TQuery, TResult>;

            if (handler == null) {
                throw new QueryHandlerNotFoundException(typeof(TQuery));
            }

            return handler.Execute(query, pageOption);
        }
    }
}