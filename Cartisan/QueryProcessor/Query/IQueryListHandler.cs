using Cartisan.Infrastructure;

namespace Cartisan.QueryProcessor.Query {
    public interface IQueryListHandler<in TQuery, TResult> where TQuery : IQuery {
        MulitiDataResult<TResult> Execute(TQuery query);
    }
}