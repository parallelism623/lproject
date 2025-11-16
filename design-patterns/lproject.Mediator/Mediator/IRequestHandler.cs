namespace lproject.Mediator.Mediator;

public interface IRequestHandler<in T, TResponse> 
where T : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(T request, CancellationToken cancellationToken = default);
}

public interface IRequestHandler<in T> 
    where T : IRequest
{
    Task HandleAsync(T request, CancellationToken cancellationToken = default);
}

