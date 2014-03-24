using Cartisan.Infrastructure;

namespace Cartisan.QueryProcessor.Query {
    public interface IQueryPageHandler<in TQuery, TResult> where TQuery: IQuery {
        Result<Paginated<TResult>> Execute(TQuery query, PageOption pageOption);
    }
}