using System;

namespace Cartisan.Exceptions {
    public class ValidateFailureException: ApplicationException {
        public ValidateFailureException(string message): base(message) {}
    }
}