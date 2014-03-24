using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cartisan.Infrastructure.Utility {
    public static class TimerTaskFactory {
        private static readonly TimeSpan DoNotRepeat = TimeSpan.FromMilliseconds(-1), Infinite = DoNotRepeat;


        public static Task<TResult> Timeout<TResult>(this Task<TResult> task, TimeSpan timeout) {
            // Short-circuit #1: infinite timeout or task already completed
            if (task.IsCompleted || (timeout == Infinite)) {
                // Either the task has already completed or timeout will never occur.
                // No proxy necessary.
                return task;
            }

            // tcs.Task will be returned as a proxy to the caller
            TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();

            // Short-circuit #2: zero timeout
            if (timeout == TimeSpan.Zero) {
                // We've already timed out.
                tcs.SetException(new TimeoutException());
                return tcs.Task;
            }

            // Set up a timer to complete after the specified timeout period
            Timer timer = null;
            timer = new Timer(_ => {
                timer.Dispose();
                // Fault our proxy with a TimeoutException
                tcs.TrySetException(new TimeoutException());
            }, null, timeout, DoNotRepeat);

            // Wire up the logic for what happens when source task completes
            task.ContinueWith(antecedent => {
                timer.Dispose();
                // Marshal results to proxy
                MarshalTaskResults(antecedent, tcs);
            });

            return tcs.Task;
        }

        public static Task<T2> ContinueWith<T1, T2>(this Task<T1> antecedentTask,
                                                    Func<Task<T1>, T2> continuationFunc,
                                                    TimeSpan timeout) {
            if (timeout == Infinite) {
                return antecedentTask.ContinueWith(t => continuationFunc(t));
            }

            TaskCompletionSource<T2> taskCompletionSource = null;
            Timer timer = null;
            timer = new Timer(_ => {
                timer.Dispose();

                // Fault our proxy Task with a TimeoutException
                taskCompletionSource.TrySetException(new TimeoutException());
            });
            taskCompletionSource = new TaskCompletionSource<T2>(timer);

            antecedentTask.ContinueWith<T2>(t => {
                timer.Change(timeout, DoNotRepeat);
                return continuationFunc(t);
            })
            .ContinueWith(t => {
                timer.Dispose();
                MarshalTaskResults(t, taskCompletionSource);
            });
            return taskCompletionSource.Task;
        }

        public static Task<T2> ContinueWith<T1, T2>(this Task<T1> antecedentTask,
                                                   Func<Task<T1>, T2> continuationFunc,
                                                   Func<Task<T1>, T2, bool> predicate,
                                                   TimeSpan pollInterval,
                                                   TimeSpan timeout) {
            Timer timer = null;
            TaskCompletionSource<T2> taskCompletionSource = null;
            DateTime expirationTime = DateTime.MinValue;

            timer =
                new Timer(_ => {
                    try {
                        if (DateTime.UtcNow > expirationTime) {
                            timer.Dispose();
                            taskCompletionSource.SetResult(default(T2));
                            return;
                        }

                        var result = continuationFunc(antecedentTask);

                        if (predicate(antecedentTask, result)) {
                            timer.Dispose();
                            taskCompletionSource.SetResult(result);
                        }
                        else {
                            // try again
                            timer.Change(pollInterval, DoNotRepeat);
                        }
                    }
                    catch (Exception e) {
                        timer.Dispose();
                        taskCompletionSource.SetException(e);
                    }
                });

            taskCompletionSource = new TaskCompletionSource<T2>(timer);


            antecedentTask.ContinueWith(t => {
                expirationTime = DateTime.UtcNow.Add(timeout);
                timer.Change(pollInterval, DoNotRepeat);
            });

            return taskCompletionSource.Task;
        }

        internal static void MarshalTaskResults<TResult>(Task source, TaskCompletionSource<TResult> proxy) {
            switch (source.Status) {
                case TaskStatus.Faulted:
                    proxy.TrySetException(source.Exception);
                    break;
                case TaskStatus.Canceled:
                    proxy.TrySetCanceled();
                    break;
                case TaskStatus.RanToCompletion:
                    Task<TResult> castedSource = source as Task<TResult>;
                    proxy.TrySetResult(
                        castedSource == null ? default(TResult) : // source is a Task
                            castedSource.Result); // source is a Task<TResult>
                    break;
            }
        }

        /// <summary>
        /// Starts a new task that will poll for a result using the specified function, and will be completed when it satisfied the specified condition.
        /// </summary>
        /// <typeparam name="T">The type of value that will be returned when the task completes.</typeparam>
        /// <param name="getResult">Function that will be used for polling.</param>
        /// <param name="isResultValid">Predicate that determines if the result is valid, or if it should continue polling</param>
        /// <param name="pollInterval">Polling interval.</param>
        /// <param name="timeout">The timeout interval.</param>
        /// <returns>The result returned by the specified function, or <see langword="null"/> if the result is not valid and the task times out.</returns>
        public static Task<T> StartNew<T>(Func<T> getResult, Func<T, bool> isResultValid, TimeSpan pollInterval, TimeSpan timeout) {
            Timer timer = null;
            TaskCompletionSource<T> taskCompletionSource = null;
            DateTime expirationTime = DateTime.UtcNow.Add(timeout);

            timer =
                new Timer(_ => {
                    try {
                        if (DateTime.UtcNow > expirationTime) {
                            timer.Dispose();
                            taskCompletionSource.SetResult(default(T));
                            return;
                        }

                        var result = getResult();

                        if (isResultValid(result)) {
                            timer.Dispose();
                            taskCompletionSource.SetResult(result);
                        }
                        else {
                            // try again
                            timer.Change(pollInterval, DoNotRepeat);
                        }
                    }
                    catch (Exception e) {
                        timer.Dispose();
                        taskCompletionSource.SetException(e);
                    }
                });

            taskCompletionSource = new TaskCompletionSource<T>(timer);

            timer.Change(pollInterval, DoNotRepeat);

            return taskCompletionSource.Task;
        }
    }
}