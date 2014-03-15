namespace Cartisan.Validation {
    public class ValidationMessage {
        public MessageType Type { get; set; }
        public string Message { get; set; }
        public string FieldName { get; set; }

        public ValidationMessage(MessageType type, string message, string fieldName = null) {
            this.Type = type;
            this.Message = message;
            this.FieldName = fieldName;
        }
    }
}