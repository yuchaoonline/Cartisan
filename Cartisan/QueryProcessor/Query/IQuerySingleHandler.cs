using Cartisan.Infrastructure;

namespace Cartisan.QueryProcessor.Query {
    public interface IQuerySingleHandler<in TQuery, TResult> where TQuery : IQuery {
        Result<TResult> Execute(TQuery query);
    }
}