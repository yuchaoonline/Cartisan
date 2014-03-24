using System.Collections.Generic;
using System.Dynamic;

namespace Cartisan.Infrastructure {
    /// <summary>
    /// 表示运行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> {
        private bool _success;
        private string _message;
        private string _status;
        private T _data;

        public Result() {
            this.Success = true;
        }

        public Result(bool success) {
            this.Success = success;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success {
            get { return this._success; }
            set {
                this._success = value;
                this._status = value ? ResultStatus.Success : ResultStatus.RuntimeFailure;
            }
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message {
            get { return this._message; }
            set { this._message = value; }
        }

        /// <summary>
        /// 结果数据
        /// </summary>
        public T Data {
            get { return this._data; }
            set { this._data = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status {
            get { return this._status; }
            set { this._status = value; }
        }
    }

    /// <summary>
    /// 动态数据类型结果
    /// </summary>
    public class Result: Result<dynamic> {
        public Result() {
            Data = new ExpandoObject();
        }

        public Result(bool success): base(success) {
            Data = new ExpandoObject();
        }
    }

    /// <summary>
    /// 多数据运行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MulitiDataResult<T>: Result<List<T>> {
        public MulitiDataResult() {
            Data = new List<T>();
        }

        public MulitiDataResult(bool success): base(success) {
            Data = new List<T>();
        }
    }
}