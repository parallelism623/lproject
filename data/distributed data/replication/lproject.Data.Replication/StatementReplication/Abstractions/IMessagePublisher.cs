namespace lproject.Data.Replication.Abstractions;

public interface IMessagePublisher : IDisposable
{
    Task PublishAsync<T>(string topic, T message, CancellationToken token = default); 
}