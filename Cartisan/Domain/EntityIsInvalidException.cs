using System;

namespace Cartisan.Domain {
    public class EntityIsInvalidException: Exception {
        public EntityIsInvalidException(string message)
            : base(message) {

        }
    }
}