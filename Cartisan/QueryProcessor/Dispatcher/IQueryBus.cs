using Cartisan.QueryProcessor.Query;

namespace Cartisan.QueryProcessor.Dispatcher {
    public interface IQueryBus {
        ISingleResult<TResult> QuerySingle<TQuery, TResult>(TQuery query) where TQuery : IQuery;
        IListResult<TResult> QueryList<TQuery, TResult>(TQuery query) where TQuery : IQuery;
        IPageResult<TResult> QueryPager<TQuery, TResult>(TQuery query, PageOption pageOption) where TQuery : IQuery;
    }
}