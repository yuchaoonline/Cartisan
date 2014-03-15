using System;
using Cartisan.Infrastructure;
using YouQiu.Framework.CommandProcessor.Command;

namespace Cartisan.CommandProcessor.Command {
    public abstract class ValidationHandlerBase  {
        protected virtual IValidationResult ExecuteResult(Action<ValidationResult> action) {
            try {
                var result = new ValidationResult {
                    Success = true 
                };
                action(result);
                return result;
            }
            catch(Exception ex) {
                //LogHelper.Log.Error(null, ex);

                return new ValidationResult{
                    Success = false,
                    State = ResultState.Exception,
                    Message = ex.Message
                };
            }
        }
    }
}