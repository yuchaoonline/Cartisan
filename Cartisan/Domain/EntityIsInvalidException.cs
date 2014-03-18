using System;

namespace Cartisan.Domain {
    public class EntityIsInvalidException: ApplicationException {
        public EntityIsInvalidException(string message)
            : base(message) {

        }
    }
}