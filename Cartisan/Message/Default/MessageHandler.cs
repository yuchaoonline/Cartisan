/*namespace Cartisan.Message.Default {
    public class MessageHandler<TMessage>: IMessageHandler where TMessage: class {
        private readonly IMessageHandler<TMessage> _messageHandler;

        public MessageHandler(IMessageHandler<TMessage> messageHandler) {
            this._messageHandler = messageHandler;
        }

        public void Handle(object message) {
            _messageHandler.Handle(message as TMessage);
        }
    }
}*/