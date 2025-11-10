using Microsoft.Extensions.Hosting;

namespace lproject.HostConfiguration.HostedServices;

public class SampleHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;

    public SampleHostedService(IHostApplicationLifetime applicationLifetime)
    {
        _applicationLifetime = applicationLifetime; 
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _applicationLifetime.ApplicationStarted.Register(OnStarted);
        _applicationLifetime.ApplicationStopping.Register(OnStopping);  
        _applicationLifetime.ApplicationStopped.Register(OnStopped);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        Console.WriteLine("Started");
    }
    private void OnStopping()
    {
        Console.WriteLine("Stopping");
    }
    private void OnStopped()
    {
        Console.WriteLine("Stopped");
    }
}