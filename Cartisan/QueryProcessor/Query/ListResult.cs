using System.Collections.Generic;

namespace Cartisan.QueryProcessor.Query {
    public class ListResult<TResult> : DefaultQueryResult, IListResult<TResult> {
        public ListResult() {}
        public ListResult(bool success): base(success) {}
        public IEnumerable<TResult> Datas { get; set; }
    }
}