using Cartisan.Result;

namespace Cartisan.CommandProcessor.Command {
    public interface ICommandResult : IResult {
    }

    public interface ICommandResult<TResult> : ICommandResult {
        TResult Data { get; set; }
    }
}