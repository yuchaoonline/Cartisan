using System;

namespace Cartisan.Infrastructure {
    public class Disposable: IDisposable {
        private bool _isDisposed;

        ~Disposable() {
            this.Dispose(false);
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if (!_isDisposed && disposing) {
                this.DisposeCore();
            }

            _isDisposed = true;
        }

        protected virtual void DisposeCore() {

        }
    }
}