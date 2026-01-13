using lproject.Data.Replication.Abstractions;
using lproject.Data.Replication.Entities;
using lproject.Data.Replication.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lproject.Data.Replication.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(LeaderDbContext dbContext, FollowerDbContext followerDbContext, IMessagePublisher messagePublisher): Controller
{
    [HttpPost]
    public async Task<IActionResult> AddAsync(Order order, CancellationToken cancellationToken)
    {
        dbContext.Orders.Add(order);
        await  dbContext.SaveChangesAsync(cancellationToken);
        var messageReplication = new ReplicationMessage<Order>
        {
            Status = Status.Added,
            Payload = order
        };
        await messagePublisher.PublishAsync(nameof(Order), messageReplication,
            token: cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        return Ok(await followerDbContext.Orders.ToListAsync(cancellationToken: cancellationToken));
    }
}