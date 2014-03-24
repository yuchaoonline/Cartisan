using System;
using Cartisan.Infrastructure;

namespace Cartisan.CommandProcessor.Command {
    public abstract class ValidationHandlerBase  {
        protected virtual Result ExecuteResult(Action<Result> action) {
            try {
                var result = new Result {
                    Success = true 
                };
                action(result);
                return result;
            }
            catch(Exception ex) {
                //LogHelper.Log.Error(null, ex);

                return new Result{
                    Success = false,
                    Status = ResultStatus.Exception,
                    Message = ex.Message
                };
            }
        }
    }
}