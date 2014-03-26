using System;
using Cartisan.Infrastructure;

namespace Cartisan.Command {
    public class CommandHandlerBase {
        protected virtual Result ExecuteResult(Action<Result> action) {
            try {
                var result = new Result{
                    Success = true
                };
                action(result);
                return result;
            }
            catch(Exception ex) {
                //LogHelper.Log.Error(null, ex);

                return new Result {
                    Success = false,
                    Status = ResultStatus.Exception,
                    Message = ex.Message
                };
            }
        }
    }
    
    public class CommandHandlerBase<TResult> {
        protected virtual Result<TResult> ExecuteResult(Action<Result<TResult>> action) {
            try {
                var result = new Result<TResult>{
                    Success = true
                };
                action(result);
                return result;
            }
            catch(Exception ex) {
                //LogHelper.Log.Error(null, ex);

                return new Result<TResult> {
                    Success = false,
                    Status = ResultStatus.Exception,
                    Message = ex.Message
                };
            }
        }
    }
}