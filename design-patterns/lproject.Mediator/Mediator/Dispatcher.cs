using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Reflection;
using static lproject.Mediator.Mediator.Utils;
namespace lproject.Mediator.Mediator;

public class Dispatcher : IDispatcher
{
    private readonly ConcurrentDictionary<Type, WrapperRequest>  _requests = new();
    private readonly ConcurrentDictionary<Type, WrapperNotify> _notifies = new();
    private readonly IServiceProvider _serviceProvider;
    private readonly IReadOnlyList<Assembly> _assemblies;
    public Dispatcher(IServiceProvider serviceProvider, params Assembly[]  assemblies)
    {
        _serviceProvider = serviceProvider;
        _assemblies =  assemblies;
    }
    
    
    
    public Task DispatchAsync(IRequest request, CancellationToken cancellationToken = default)
    {
        var handler = _requests.GetOrAdd(request.GetType(), GetWrapperRequest);
        return  handler.Invoke(request, _serviceProvider, cancellationToken);
    }

    public async Task<TResponse> DispatchAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handler = _requests.GetOrAdd(request.GetType(), GetWrapperRequest);
        var result = await handler.Invoke(request, _serviceProvider, cancellationToken);
        return (TResponse)result!;
    }

    public Task NotifyAsync(INotify request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private WrapperRequest GetWrapperRequest(Type requestType)
    {
        var handlerTypes = _assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(IsRequestHandlerType)
            .ToList();

        var handlerType = handlerTypes
                              .FirstOrDefault(t =>
                                  t.GetInterfaces().Any(i =>
                                      i.IsGenericType &&
                                      (
                                          i.GetGenericTypeDefinition() == typeof(IRequestHandler<>) ||
                                          i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)
                                      ) &&
                                      i.GetGenericArguments()[0] == requestType
                                  ))
                          ?? throw new InvalidOperationException(
                              $"No handler found for {requestType.FullName}");

        var handlerInterface = handlerType.GetInterfaces()
            .First(i =>
                i.IsGenericType &&
                (
                    i.GetGenericTypeDefinition() == typeof(IRequestHandler<>) ||
                    i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)
                ) &&
                i.GetGenericArguments()[0] == requestType);

        var method = handlerInterface.GetMethod("HandleAsync")
                     ?? throw new InvalidOperationException(
                         $"HandleAsync not found on {handlerInterface.Name}");

        return new WrapperRequest(method, handlerInterface);
    }
}