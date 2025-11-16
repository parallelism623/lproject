namespace lproject.Mediator.Mediator;

public interface IDispatcher{
    Task DispatchAsync(IRequest request, CancellationToken cancellationToken = default);
    Task<TResponse> DispatchAsync<TResponse>(IRequest<TResponse> request,  CancellationToken cancellationToken = default);
    
    Task NotifyAsync(INotify request, CancellationToken cancellationToken = default);
}