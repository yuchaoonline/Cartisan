using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cartisan.Infrastructure.Extensions;
using Cartisan.Message;

namespace Cartisan.EntityFramework.MessageStoring {
    public abstract class Message {
        protected Message() { }

        protected Message(IMessageContext messageContext, string sourceMessageId) {
            Id = messageContext.MessageId;
            SourceMessageId = sourceMessageId;
            MessageBody = messageContext.Message.ToJson();
            Created = messageContext.SentTime;
            Name = messageContext.Message.GetType().Name;
            Type = messageContext.Message.GetType().FullName;
        }

        public string Id { get; set; }
        public string SourceMessageId { get; set; }
        public string MessageBody { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        [ForeignKey("SourceMessageId")]
        public virtual Message ParentMessage { get; set; }
        [InverseProperty("ParentMessage")]
        public virtual ICollection<Message> ChildrenMessages { get; set; }
    }
}