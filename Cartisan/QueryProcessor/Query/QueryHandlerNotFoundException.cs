using System;

namespace Cartisan.QueryProcessor.Query {
    public class QueryHandlerNotFoundException  : Exception {
        public QueryHandlerNotFoundException(Type type)
            : base(string.Format("未找到查询处理器：{0}。", type)) { }
    }
}