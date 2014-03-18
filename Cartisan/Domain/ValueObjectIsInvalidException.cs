using System;

namespace Cartisan.Domain {
    public class ValueObjectIsInvalidException: ApplicationException {
        public ValueObjectIsInvalidException(string message)
            : base(message) {

        }
    }
}