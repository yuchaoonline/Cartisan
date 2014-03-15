namespace Cartisan.QueryProcessor.Query {
    public interface ISingleResult<TResult> : IQueryResult {
        TResult Data { get; set; }
    }
}