using System;

namespace Cartisan.Command {
    public class ValidationHandlerNotFoundException : ApplicationException {
        public ValidationHandlerNotFoundException(Type type)
            : base(string.Format("未找到的验证处理器：{0}。", type)) { }
    }
}