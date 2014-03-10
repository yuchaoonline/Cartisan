/*using System.Threading;
using System.Threading.Tasks;

namespace Cartisan.Message {
    public class MessageState {
        public MessageState() {}

        public MessageState(IMessageContext messageContext) {
            this.MessageContext = messageContext;
        }

        public IMessageContext MessageContext { get; set; }
        public string MessageId { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public TaskCompletionSource<object> TaskCompletionSource { get; set; } 
    }
}*/