using System;
using System.Collections.Generic;

namespace Cartisan.Bus {
    public interface IBus<in TMessage>: IDisposable {
        void Commit();
        void Publish<TConcreteMessage>(TConcreteMessage concreteMessage) where TConcreteMessage: TMessage;
        void Publish<TConcreteMessage>(IEnumerable<TConcreteMessage> concreteMessages) where TConcreteMessage: TMessage;
    }
}