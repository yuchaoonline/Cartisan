namespace Cartisan.Command.Default {
    public class CommandBase: ICommand {
        public bool NeedRetry { get; set; }

        public CommandBase() {
            NeedRetry = false;
        }
    }

    public abstract class LinearCommandBase: CommandBase, ILinearCommand { }
}