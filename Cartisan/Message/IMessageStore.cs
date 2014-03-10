using System;
using System.Collections.Generic;

namespace Cartisan.Message {
    public interface IMessageStore: IDisposable {
        void Save(IMessageContext commandContext, string domainEventId);
        void Save(IMessageContext commandContext, IEnumerable<IMessageContext> domainEventContexts);
    }
}