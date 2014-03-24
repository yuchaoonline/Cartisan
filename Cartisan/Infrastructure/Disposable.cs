using System;

namespace Cartisan.Infrastructure {
    public class Disposable: IDisposable {
        private bool _disposed;

        ~Disposable() {
            this.Dispose(false);
        }

        public void Dispose() {
            // 所有资源被释放
            this.Dispose(true);
            // 不需要再调用本对象的Finalize()方法
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        /// <param name="disposing">true: 清理托管资源；false:只清理非托管资源</param>
        private void Dispose(bool disposing) {
            if (!this._disposed) {
                if(disposing) {
                    this.DisposeManaged();
                }
                this.DisposeUnmanaged();
            }

            // 已经释放过，用于防止重复执行释放逻辑
            this._disposed = true;
        }

        /// <summary>
        /// 清理托管资源
        /// </summary>
        protected virtual void DisposeManaged() { }

        /// <summary>
        /// 清理非托管资源
        /// </summary>
        protected virtual void DisposeUnmanaged() { }
    }
}