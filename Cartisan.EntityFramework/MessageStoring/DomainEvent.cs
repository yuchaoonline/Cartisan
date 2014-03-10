using System.Collections.Generic;
using System.Linq;
using Cartisan.Event;
using Cartisan.Message;

namespace Cartisan.EntityFramework.MessageStoring {
    public class DomainEvent: Message {
        public string AggregateRootId { get; set; }
        public string AggregateRootType { get; set; }

        protected DomainEvent() {}

        public DomainEvent(IMessageContext messageContext, string sourceMessageId)
            : base(messageContext, sourceMessageId) {
            IDomainEvent domainEvent = messageContext.Message as IDomainEvent;
            AggregateRootId = domainEvent.AggregateRootId.ToString();
            AggregateRootType = domainEvent.AggregateRootName;
        }

        public Command Parent {
            get { return ParentMessage as Command; }
        }

        public IEnumerable<Command> Children {
            get { return ChildrenMessages.Cast<Command>(); }
        } 
    }
}