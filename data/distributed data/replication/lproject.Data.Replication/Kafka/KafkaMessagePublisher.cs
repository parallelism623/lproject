using Confluent.Kafka;
using lproject.Data.Replication.Abstractions;
using lproject.HostConfiguration.ConfigurationAccessor;
using System.Text.Json;

namespace lproject.Data.Replication.Kafka;

public class KafkaMessagePublisher : IMessagePublisher
{
    private bool _disposed = true;
    private readonly IProducer<string, string> _producer;

    public KafkaMessagePublisher(IConfigurationAccessor configuration)
    {
        var publisherSettings = configuration.GetValue<KafkaPublisherSettings>("Kafka:PublisherSettings");
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = publisherSettings.BoostrapServer,
            Acks = Acks.All
        };

        _producer = new ProducerBuilder<string, string>(producerConfig).Build();
    }
    public async Task PublishAsync<T>(string topic, T message, CancellationToken token = default)
    {
        var json = JsonSerializer.Serialize(message);
        await _producer.ProduceAsync(topic, new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = json
        }, token);
    }
    
    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _producer?.Dispose();
        }
        _disposed = true;
    }
}