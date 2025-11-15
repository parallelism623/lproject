namespace lproject.Concurrency.CancellationToken;

public class AsyncQueueExample<T>
{
    private readonly Queue<TaskCompletionSource<T>> _waitingQueueForProcessed =  new ();
    private readonly Queue<T> _queueItems = new ();
    private readonly Lock _lock = new ();
    public Task<T> Dequeue(System.Threading.CancellationToken ct = default)
    {
        lock (_lock)
        {
            if (_queueItems.Count > 0) { return Task.FromResult(_queueItems.Dequeue()); }

            var tcs = new TaskCompletionSource<T>(
                TaskCreationOptions.RunContinuationsAsynchronously); 
            _waitingQueueForProcessed.Enqueue(tcs);
            if (ct.CanBeCanceled)
            {
                ct.Register(() =>
                {
                    tcs.TrySetCanceled();
                });
            }

            return tcs.Task;
        }
    }

    public void Enqueue(T item)
    {
        lock (_lock)
        {
            if (_waitingQueueForProcessed.Count > 0)
            {
                var processor = _waitingQueueForProcessed.Dequeue();
                if (processor.TrySetResult(item))
                {
                    return;
                }
            }
            _queueItems.Enqueue(item);
        }
    }
}