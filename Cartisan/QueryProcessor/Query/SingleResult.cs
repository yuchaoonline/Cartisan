namespace Cartisan.QueryProcessor.Query {
    public class SingleResult<TResult>: DefaultQueryResult, ISingleResult<TResult> {
        public SingleResult() {}

        public SingleResult(bool success): base(success) {}

        public TResult Data { get; set; }
    }
}