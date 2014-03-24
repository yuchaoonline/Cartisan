using Cartisan.CommandProcessor.Command;
using Cartisan.Infrastructure;

namespace Cartisan.CommandProcessor.Dispatcher {
    public interface ICommandBus {
        Result Submit<TCommand>(TCommand command) where TCommand : ICommand;
        Result Validate<TCommand>(TCommand command) where TCommand : ICommand;
        
        Result<TResult> Submit<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    }
}