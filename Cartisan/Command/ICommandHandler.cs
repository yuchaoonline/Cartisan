using Cartisan.Infrastructure;

namespace Cartisan.Command {
    public interface ICommandHandler<in TCommand> where TCommand: ICommand {
        Result Execute(TCommand command);
    }

    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand {
        Result<TResult> Execute(TCommand command);
    }
}