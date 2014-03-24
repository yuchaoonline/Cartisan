using Cartisan.Infrastructure;
using Cartisan.QueryProcessor.Query;

namespace Cartisan.QueryProcessor.Dispatcher {
    public interface IQueryBus {
        Result<TResult> QuerySingle<TQuery, TResult>(TQuery query) where TQuery : IQuery;
        MulitiDataResult<TResult> QueryList<TQuery, TResult>(TQuery query) where TQuery : IQuery;
        Result<Paginated<TResult>> QueryPager<TQuery, TResult>(TQuery query, PageOption pageOption) where TQuery : IQuery;
    }
}