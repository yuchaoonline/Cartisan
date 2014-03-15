namespace Cartisan.CommandProcessor.Command {
    public interface IValidationHandler<in TCommand> where TCommand: ICommand {
        IValidationResult Validate(TCommand command);
    }
}