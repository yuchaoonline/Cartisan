using Cartisan.Result;

namespace Cartisan.CommandProcessor.Command {
    public class CommandResult : DefaultResult, ICommandResult {
        public CommandResult() {}
        public CommandResult(bool success): base(success) {}
    }
    
    public class CommandResult<TResult> : DefaultResult, ICommandResult<TResult> {
        public CommandResult() {}
        public CommandResult(bool success): base(success) {}

        public TResult Data { get; set; }
    }
}