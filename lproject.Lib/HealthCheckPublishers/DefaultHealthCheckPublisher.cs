using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace lproject.Lib.HealthCheckPublishers;
public class DefaultHealthCheckPublisher : IHealthCheckPublisher
{
    public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
    {
        Console.WriteLine("Test");
        return Task.CompletedTask;
    }
}
