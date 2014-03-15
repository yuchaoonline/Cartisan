using Cartisan.Result;

namespace Cartisan.QueryProcessor.Query {
    public class DefaultQueryResult : DefaultResult, IQueryResult {
        public DefaultQueryResult() {}
        public DefaultQueryResult(bool success): base(success) {}
    }
}