namespace Cartisan.QueryProcessor.Query {
    public interface IQueryListHandler<in TQuery, TResult> where TQuery : IQuery {
        IListResult<TResult> Execute(TQuery query);
    }
}