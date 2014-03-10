using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cartisan.Message {
    public interface IMessageContext {
        IDictionary<string, string> Headers { get; }
        string Key { get; }
        string MessageId { get; }
        string ReplyToEndPoint { get; }
        object Reply { get; set; }
        string FromEndPoint { get; set; }
        [JsonIgnore]
        object Message { get; }
        DateTime SentTime { get; }
    }
}