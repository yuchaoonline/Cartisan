using System;

namespace Cartisan.Domain {
    public class ValueObjectIsInvalidException: Exception {
        public ValueObjectIsInvalidException(string message)
            : base(message) {

        }
    }
}