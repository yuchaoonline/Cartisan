using System;
using System.Runtime.Serialization;

namespace Cartisan.Infrastructure {
    public enum ErrorCode {
        Exception,
        RuntimeFailure,
        Unauthorized,
        ValidateFailure
    }

    [Serializable]
    public class CartisanException: ApplicationException {
        public ErrorCode ErrorCode { get; set; }

        protected CartisanException(SerializationInfo info, StreamingContext context): base(info, context) {
            ErrorCode = (ErrorCode)info.GetValue("ErrorCode", typeof(ErrorCode));
        }

        public CartisanException(ErrorCode errorCode, string message = null, Exception innerException = null)
            : base(message ?? errorCode.ToString(), innerException) {
            ErrorCode = errorCode;
        }

        public CartisanException() {}

        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("ErrorCode", ErrorCode);
            base.GetObjectData(info, context);
        }
    }
}