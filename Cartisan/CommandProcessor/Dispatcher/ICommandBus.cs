using Cartisan.CommandProcessor.Command;
using YouQiu.Framework.CommandProcessor.Command;

namespace YouQiu.Framework.CommandProcessor.Dispatcher {
    public interface ICommandBus {
        ICommandResult Submit<TCommand>(TCommand command) where TCommand : ICommand;
        IValidationResult Validate<TCommand>(TCommand command) where TCommand : ICommand;
        
        ICommandResult<TResult> Submit<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    }
}