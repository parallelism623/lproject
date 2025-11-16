using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static lproject.Mediator.Mediator.Utils;

namespace lproject.Mediator.Mediator;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        AddMediator(services, Assembly.GetExecutingAssembly());
        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies)
    {
        InitialComponents(services, assemblies);
        services.AddScoped<IDispatcher, Dispatcher>(sp => new Dispatcher(sp, assemblies));
        return services;
    }

    static void InitialComponents(IServiceCollection services, params Assembly[] assemblies)
    {
        var allTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .ToArray();
        
        var handlerTypes = allTypes
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition)
            .Where(IsRequestHandlerType)
            .ToList();
        
        foreach (var handlerType in handlerTypes)
        {
            var handlerInterfaces = handlerType
                .GetInterfaces()
                .Where(IsRequestHandlerInterface)
                .ToList();

            foreach (var itf in handlerInterfaces)
            {
                services.AddScoped(itf, handlerType);
            }
        }
    }
}