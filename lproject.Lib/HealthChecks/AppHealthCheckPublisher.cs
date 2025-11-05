using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace lproject.Lib.HealthChecks;
public class AppHealthCheckPublisher : IHealthCheckPublisher
{
    public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
    {
        Console.WriteLine("App");
        return Task.CompletedTask;
    }
}
