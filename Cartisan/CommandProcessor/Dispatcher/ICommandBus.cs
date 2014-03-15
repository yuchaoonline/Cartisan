using Cartisan.CommandProcessor.Command;

namespace Cartisan.CommandProcessor.Dispatcher {
    public interface ICommandBus {
        ICommandResult Submit<TCommand>(TCommand command) where TCommand : ICommand;
        IValidationResult Validate<TCommand>(TCommand command) where TCommand : ICommand;
        
        ICommandResult<TResult> Submit<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    }
}