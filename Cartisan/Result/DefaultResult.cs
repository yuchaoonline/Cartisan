using Cartisan.Infrastructure;

namespace Cartisan.Result {
    public class DefaultResult : IResult {
        private bool _success;
        private ResultState _state;
        private string _message;

        public DefaultResult() {
            this.Success = true;
        }

        public DefaultResult(bool success) {
            this.Success = success;
        }

        public bool Success {
            get { return this._success; }
            set {
                this._success = value;
                this._state = value ? ResultState.Success : ResultState.RuntimeFailure;
            }
        }

        public string Message {
            get { return this._message; }
            set { this._message = value; }
        }

        public ResultState State {
            get { return this._state; }
            set { this._state = value; }
        }
    }
}