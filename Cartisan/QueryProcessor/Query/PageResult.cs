using System.Collections.Generic;

namespace Cartisan.QueryProcessor.Query {
    public class PageResult<TResult> : DefaultQueryResult, IPageResult<TResult> {
        public PageResult() {}
        public PageResult(bool success): base(success) {}

        public Pager Pager { get; set; }

        public IEnumerable<TResult> Datas { get; set; }
    }
}