using Cartisan.CommandProcessor.Command;
using StructureMap;
using YouQiu.Framework.CommandProcessor.Command;
using YouQiu.Framework.CommandProcessor.Dispatcher;

namespace Cartisan.StructureMap {
    public class StructureMapCommandBus: ICommandBus {
        public ICommandResult Submit<TCommand>(TCommand command) where TCommand: ICommand {
            var handler = ObjectFactory.GetInstance(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;
            if (handler == null) {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
            return handler.Execute(command);
        }

        public IValidationResult Validate<TCommand>(TCommand command) where TCommand: ICommand {
            var handler = ObjectFactory.GetInstance(typeof(IValidationHandler<TCommand>)) as IValidationHandler<TCommand>;
            if (handler == null) {
                throw new ValidationHandlerNotFoundException(typeof(TCommand));
            }
            return handler.Validate(command);
        }

        public ICommandResult<TResult> Submit<TCommand, TResult>(TCommand command) where TCommand: ICommand {
            var handler = ObjectFactory.GetInstance(typeof(ICommandHandler<TCommand, TResult>)) as ICommandHandler<TCommand, TResult>;
            if (handler == null) {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
            return handler.Execute(command);
        }
    }
}