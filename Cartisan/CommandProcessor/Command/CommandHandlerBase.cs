using System;
using Cartisan.Infrastructure;
using YouQiu.Framework.CommandProcessor.Command;

namespace Cartisan.CommandProcessor.Command {
    public class CommandHandlerBase {
        protected virtual ICommandResult ExecuteResult(Action<CommandResult> action) {
            try {
                var result = new CommandResult{
                    Success = true
                };
                action(result);
                return result;
            }
            catch(Exception ex) {
                //LogHelper.Log.Error(null, ex);

                return new CommandResult {
                    Success = false,
                    State = ResultState.Exception,
                    Message = ex.Message
                };
            }
        }
    }
    
    public class CommandHandlerBase<TResult> {
        protected virtual ICommandResult<TResult> ExecuteResult(Action<CommandResult<TResult>> action) {
            try {
                var result = new CommandResult<TResult>{
                    Success = true
                };
                action(result);
                return result;
            }
            catch(Exception ex) {
                //LogHelper.Log.Error(null, ex);

                return new CommandResult<TResult> {
                    Success = false,
                    State = ResultState.Exception,
                    Message = ex.Message
                };
            }
        }
    }
}