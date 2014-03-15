namespace Cartisan.QueryProcessor.Query {
    public interface IQueryPageHandler<in TQuery, TResult> where TQuery: IQuery {
        IPageResult<TResult> Execute(TQuery query, PageOption pageOption);
    }
}