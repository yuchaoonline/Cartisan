using System.Collections.Generic;

namespace Cartisan.QueryProcessor.Query {
    public interface IListResult<TResult> : IQueryResult {
        IEnumerable<TResult> Datas { get; set; } 
    }
}