/*using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cartisan.Infrastructure {
    public class TaskWorker {
        protected Task _task;
        protected object _mutex = new object();
        protected Semaphore _semaphore = new Semaphore(0, 1);
        protected CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        protected volatile bool _toExit = false;
        protected volatile bool _suspend = false;
        protected WorkDelegate _workDelegate;

        protected int _workInterval = 0;
        public int WorkInterval {
            get { return _workInterval; }
            set { _workInterval = value; }
        }

        protected void Sleep(int timeout) {
            Thread.Sleep(timeout);
        }

        public delegate void WorkDelegate();

        protected virtual void Run() {
            while (!_toExit) {
                try {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    if (_suspend) {
                        _semaphore.WaitOne();
                        _suspend = false;
                    }
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    if (_workDelegate != null) {
                        _workDelegate.Invoke();
                    }
                    else {
                        Work();
                    }
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    if (WorkInterval > 0) {
                        Sleep(WorkInterval);
                    }
                }
                catch (OperationCanceledException) {
                    break;
                }
                catch (ThreadInterruptedException) {
                    break;
                }
                catch (Exception ex) {
                    Console.Write(ex.Message);
                }
            }
        }

        protected virtual void Work() {

        }


        public virtual void Suspend() {
            _suspend = true;
        }

        public virtual void Resume() {
            lock (_mutex) {
                try {
                    _semaphore.Release();
                }
                catch (Exception) {

                }
            }
        }

        public TaskWorker() {
        }

        public TaskWorker(WorkDelegate run) {
            _workDelegate = run;
        }

        public virtual TaskWorker Start() {
            lock (_mutex) {
                if (_task == null) {
                    _task = Task.Factory.StartNew(Run, _cancellationTokenSource.Token);
                }
            }
            return this;
        }

        public virtual void Wait(int millionSecondsTimeout = 0) {
            if (millionSecondsTimeout > 0) {
                Task.WaitAll(new Task[] { _task }, millionSecondsTimeout);
            }
            else {
                Task.WaitAll(_task);
            }
        }

        public virtual void Stop(bool forcibly = false) {
            lock (_mutex) {
                if (!_toExit) {
                    _toExit = true;
                    if (_suspend) {
                        Resume();
                    }
                    if (forcibly) {
                        _cancellationTokenSource.Cancel(true);
                    }
                }
            }
        }

        public TaskStatus GetState() {
            return _task.Status;
        }
    }
}*/