﻿using System;

namespace Cartisan.CommandProcessor.Command {
    public class ValidationHandlerNotFoundException : Exception {
        public ValidationHandlerNotFoundException(Type type)
            : base(string.Format("未找到的验证处理器：{0}。", type)) { }
    }
}