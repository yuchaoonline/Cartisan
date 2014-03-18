using System;

namespace Cartisan.Exceptions {
    public class RuntimeFailureException: ApplicationException {
        public RuntimeFailureException(string message): base(message) {}
    }
}