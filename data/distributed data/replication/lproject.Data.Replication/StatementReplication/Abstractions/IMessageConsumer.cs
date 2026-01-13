namespace lproject.Data.Replication.Abstractions;


public interface IMessageConsumer<T> : IDisposable
{
    Task SubscribeAsync(Func<T, CancellationToken, Task> handler, CancellationToken token = default);
}