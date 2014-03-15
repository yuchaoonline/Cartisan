namespace Cartisan.QueryProcessor.Query {
    public interface IQuerySingleHandler<in TQuery, TResult> where TQuery : IQuery {
        ISingleResult<TResult> Execute(TQuery query);
    }
}