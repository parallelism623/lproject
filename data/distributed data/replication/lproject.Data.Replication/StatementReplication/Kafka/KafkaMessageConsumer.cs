using Confluent.Kafka;
using lproject.Data.Replication.Abstractions;
using lproject.Data.Replication.Entities;
using lproject.HostConfiguration.ConfigurationAccessor;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace lproject.Data.Replication.Kafka;

public class KafkaMessageConsumer<T> : IMessageConsumer<T>
{
    private bool _disposed = false;
    private readonly IConsumer<string,string> _consumer;

    public KafkaMessageConsumer(IOptionsMonitor<KafkaConsumerSettings> configuration, string topic)
    {
        var consumerSettings = configuration.CurrentValue;
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = consumerSettings.BoostrapServer,
            GroupId = "order-service-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        _consumer = new ConsumerBuilder<string,string>(consumerConfig).Build();
        _consumer.Subscribe(nameof(Order));
    }

    public async Task SubscribeAsync(Func<T, CancellationToken, Task> handler, CancellationToken token = default)
    {
        try
        {
            var cr = _consumer.Consume(token);

            var value = cr.Message.Value;
            var order = JsonSerializer.Deserialize<T>(value);
            if(order != null)
                await handler(order, token);
            else
            {
                // Log
            }
        }
        catch (Exception ex)
        {
            var e = ex;
        }
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
            _consumer.Close();
            _consumer.Dispose();
        }
        _disposed = true;
    }
}