using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetBlocks.Utilities
{
    /// <summary>
    /// A utility class that provides debounce functionality for an action.
    /// The action will only be executed after the specified timeout has elapsed
    /// without any new calls to the debounced method.
    /// </summary>
    public class Debouncer : IDisposable
    {
        private readonly TimeSpan _delay;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly object _lockObject = new object();
        private SynchronizationContext? _synchronizationContext;

        /// <summary>
        /// Creates a new instance of the Debouncer class.
        /// </summary>
        /// <param name="delayMilliseconds">The delay in milliseconds before the action is executed.</param>
        /// <param name="useSynchronizationContext">If true, the action will be executed on the thread that created the debouncer.</param>
        public Debouncer(int delayMilliseconds, bool useSynchronizationContext = true)
        {
            _delay = TimeSpan.FromMilliseconds(delayMilliseconds);
            _cancellationTokenSource = new CancellationTokenSource();

            // Capture the current SynchronizationContext if requested
            if (useSynchronizationContext)
            {
                _synchronizationContext = SynchronizationContext.Current;
            }
        }

        /// <summary>
        /// Debounces the specified action.
        /// </summary>
        /// <param name="action">The action to debounce.</param>
        public void Debounce(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            lock (_lockObject)
            {
                // Cancel any previous debounce operation
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();

                // Create a local copy of the token and context for this specific debounce request
                var token = _cancellationTokenSource.Token;
                var context = _synchronizationContext;

                // Start a new task that will execute the action after the delay
                Task.Delay(_delay, token)
                    .ContinueWith(task =>
                    {
                        // Only execute the action if the delay completed (wasn't cancelled)
                        if (!task.IsCanceled)
                        {
                            // If we have a synchronization context, use it to execute the action
                            // This ensures the action runs on the original thread (like the UI thread)
                            if (context != null)
                            {
                                context.Post(_ => action(), null);
                            }
                            else
                            {
                                action();
                            }
                        }
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
            }
        }

        /// <summary>
        /// Cancels any pending debounced actions.
        /// </summary>
        public void Cancel()
        {
            lock (_lockObject)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

        /// <summary>
        /// Disposes the debouncer, cancelling any pending operations.
        /// </summary>
        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _synchronizationContext = null;
        }
    }
}
