using System;

namespace Cartisan.Exceptions {
    public class UnauthorizedException: ApplicationException {
        public UnauthorizedException(string message): base(message) {}
    }
}