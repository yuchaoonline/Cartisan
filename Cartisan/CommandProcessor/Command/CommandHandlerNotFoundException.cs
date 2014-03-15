﻿using System;

namespace Cartisan.CommandProcessor.Command {
    public class CommandHandlerNotFoundException : Exception {
        public CommandHandlerNotFoundException(Type type)
            : base(string.Format("未找到命令处理器：{0}。", type)) { }
    }
}