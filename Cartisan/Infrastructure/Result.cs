using System.Dynamic;

namespace Cartisan.Infrastructure {
    public class Result<T> {
         private bool _success;
        private string _message;
        private ResultState _state;
        private T _data;

        public Result() {
            this.Success = true;
        }

        public Result(bool success) {
            this.Success = success;
        }

        public bool Success {
            get { return this._success; }
            set {
                this._success = value;
                this._state = value ? ResultState.Success : ResultState.RuntimeFail;
            }
        }

        public string Message {
            get { return this._message; }
            set { this._message = value; }
        }

        public T Data {
            get { return this._data; }
            set { this._data = value; }
        }

        public ResultState State {
            get { return this._state; }
            set { this._state = value; }
        }
    }

    public class Result: Result<dynamic> {
        public Result() {
            Data = new ExpandoObject();
        }

        public Result(bool success): base(success) {
            Data = new ExpandoObject();
        }
    }
}