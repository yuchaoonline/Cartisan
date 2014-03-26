using Cartisan.Command;
using Cartisan.Infrastructure;
using StructureMap;

namespace Cartisan.StructureMap {
    public class StructureMapCommandBus: ICommandBus {
        public Result Submit<TCommand>(TCommand command) where TCommand: ICommand {
            var handler = ObjectFactory.GetInstance(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;
            if (handler == null) {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
            return handler.Execute(command);
        }

        public Result Validate<TCommand>(TCommand command) where TCommand: ICommand {
            var handler = ObjectFactory.GetInstance(typeof(IValidationHandler<TCommand>)) as IValidationHandler<TCommand>;
            if (handler == null) {
                throw new ValidationHandlerNotFoundException(typeof(TCommand));
            }
            return handler.Validate(command);
        }

        public Result<TResult> Submit<TCommand, TResult>(TCommand command) where TCommand: ICommand {
            var handler = ObjectFactory.GetInstance(typeof(ICommandHandler<TCommand, TResult>)) as ICommandHandler<TCommand, TResult>;
            if (handler == null) {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
            return handler.Execute(command);
        }
    }
}