/*using System.Collections.Generic;
using System.Linq;
using Cartisan.Event.Default;
using Cartisan.Message;

namespace Cartisan.EntityFramework.MessageStoring {
    public class Command: Message {
        protected Command() { }

        public Command(IMessageContext messageContext, string sourceMessageId): base(messageContext, sourceMessageId) {}

        public DomainEvent Parent {
            get { return ParentMessage as DomainEvent; }
        }

        public IEnumerable<DomainEvent> Children {
            get { return ChildrenMessages.Cast<DomainEvent>(); }
        } 
    }
}*/