using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace lproject.HealthChecks.HealthChecks;
public class AppHealthCheckPublisher : IHealthCheckPublisher
{
    public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
    {
        if (report.Status != HealthStatus.Healthy)
        {
            
        }
        return Task.CompletedTask;
    }
}
