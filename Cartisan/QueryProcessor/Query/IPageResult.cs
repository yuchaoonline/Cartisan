using System.Collections.Generic;

namespace Cartisan.QueryProcessor.Query {
    public interface IPageResult<TResult> : IQueryResult {
        Pager Pager { get; set; }
        IEnumerable<TResult> Datas { get; set; }
    }
}