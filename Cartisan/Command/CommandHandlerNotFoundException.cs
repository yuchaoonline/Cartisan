using System;

namespace Cartisan.Command {
    public class CommandHandlerNotFoundException : ApplicationException {
        public CommandHandlerNotFoundException(Type type)
            : base(string.Format("未找到命令处理器：{0}。", type)) { }
    }
}