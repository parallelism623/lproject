using lproject.HostConfiguration.HostedServices;
using Microsoft.Extensions.DependencyInjection;
namespace lproject.HostConfiguration;

public static class Excute
{
    public static IServiceCollection RegisterHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<SampleHostedService>();
        return services;
    }
}