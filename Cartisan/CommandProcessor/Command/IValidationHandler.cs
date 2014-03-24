using Cartisan.Infrastructure;

namespace Cartisan.CommandProcessor.Command {
    public interface IValidationHandler<in TCommand> where TCommand: ICommand {
        Result Validate(TCommand command);
    }
}