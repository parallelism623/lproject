using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace lproject.HostConfiguration.ConfigurationAccessor;

public class ConfigurationAccessor(IConfiguration configuration) : IConfigurationAccessor
{
    public T GetValue<T>(string key)
    {
        return configuration.GetSection(key).Get<T>() ?? default(T)!;
    }
}

public static class ConfigurationAccessorExtension
{
    public static IServiceCollection AddConfigurationAccessor(this IServiceCollection services, IConfiguration configuraiton)
    {
        services.AddScoped<IConfigurationAccessor, ConfigurationAccessor>();
        return services;
    }
}