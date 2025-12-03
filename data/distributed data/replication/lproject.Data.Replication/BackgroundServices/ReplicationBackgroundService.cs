using lproject.Data.Replication.Abstractions;
using lproject.Data.Replication.Entities;
using lproject.Data.Replication.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace lproject.Data.Replication.BackgroundServices;

public class ReplicationBackgroundService : BackgroundService
{
    private readonly IMessageConsumer<ReplicationMessage<Order>> _orderConsumer;
    private readonly IServiceScopeFactory _scopeFactory;

    public ReplicationBackgroundService(IServiceScopeFactory  scopeFactory,
        IOptionsMonitor<KafkaConsumerSettings> configuration)
    {
        _scopeFactory = scopeFactory;
        _orderConsumer = new KafkaMessageConsumer<ReplicationMessage<Order>>(configuration, nameof(Order));
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _orderConsumer.SubscribeAsync(ReplicationChangeAsync, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            _orderConsumer.Dispose();
        }
    }

    private async Task ReplicationChangeAsync(ReplicationMessage<Order> order, CancellationToken token)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order)); 
        }

        if (order.Status == Status.Added)
        {
            await AddOrderAsync(order.Payload, token);
        }

        if (order.Status == Status.Deleted)
        {
            await DeleteOrderAsync(order.Payload, token); 
        }
        
        if (order.Status == Status.Modified)
        {
            await UpdateOrderAsync(order.Payload, token); 
        }
    }

    private async Task AddOrderAsync(Order order, CancellationToken token)
    {
        using var scope =  _scopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetService<FollowerDbContext>()!;
        dbContext.Add(order);
        await dbContext.SaveChangesAsync(token);
    }
    private async Task UpdateOrderAsync(Order order, CancellationToken token)
    {
        using var scope =  _scopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetService<FollowerDbContext>()!;
        dbContext.Add(order);
        dbContext.Update(order);
        await dbContext.SaveChangesAsync(token);
    }
    private async Task DeleteOrderAsync(Order order, CancellationToken token)
    {
        using var scope =  _scopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetService<FollowerDbContext>()!;
        dbContext.Add(order);
        dbContext.Remove(order);
        await dbContext.SaveChangesAsync(token);
    }
}