using System;
using Cartisan.Infrastructure;

namespace Cartisan.QueryProcessor.Query {
    public abstract class QueryHandlerBase<TResult> where TResult:Result, new(){
        protected virtual TResult ExecuteResult(Action<TResult> action) {
            try {
                var result = new TResult {
                    Success = true
                };
                action(result);
                return result;
            }
            catch(Exception ex) {
                return new TResult {
                    Success = false,
                    //State = ResultState.Exception,
                    Message = ex.Message
                };
            }
        }
    }
}